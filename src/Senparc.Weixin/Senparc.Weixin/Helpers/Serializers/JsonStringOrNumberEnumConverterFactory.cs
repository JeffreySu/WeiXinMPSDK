#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.Helpers.Serializers
{
    /// <summary>
    /// Reads enum names and numbers while preserving the historical numeric JSON output.
    /// </summary>
    public sealed class JsonStringOrNumberEnumConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(JsonStringOrNumberEnumConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }

        private sealed class JsonStringOrNumberEnumConverter<TEnum> : JsonConverter<TEnum>
            where TEnum : struct, Enum
        {
            public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    return ReadNumber(ref reader);
                }

                if (reader.TokenType != JsonTokenType.String)
                {
                    throw new JsonException($"Expected a string or number for enum {typeof(TEnum).FullName}.");
                }

                var value = reader.GetString();
                if (Enum.TryParse(value, ignoreCase: true, out TEnum result))
                {
                    return result;
                }

                foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var enumMember = field.GetCustomAttribute<EnumMemberAttribute>();
                    if (!string.IsNullOrEmpty(enumMember?.Value) &&
                        string.Equals(enumMember.Value, value, StringComparison.OrdinalIgnoreCase))
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }

                throw new JsonException($"Value '{value}' is not a valid member of enum {typeof(TEnum).FullName}.");
            }

            public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
            {
                var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
                var typeCode = Type.GetTypeCode(underlyingType);
                var numericValue = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);

                switch (typeCode)
                {
                    case TypeCode.SByte:
                        writer.WriteNumberValue((sbyte)numericValue);
                        break;
                    case TypeCode.Byte:
                        writer.WriteNumberValue((byte)numericValue);
                        break;
                    case TypeCode.Int16:
                        writer.WriteNumberValue((short)numericValue);
                        break;
                    case TypeCode.UInt16:
                        writer.WriteNumberValue((ushort)numericValue);
                        break;
                    case TypeCode.Int32:
                        writer.WriteNumberValue((int)numericValue);
                        break;
                    case TypeCode.UInt32:
                        writer.WriteNumberValue((uint)numericValue);
                        break;
                    case TypeCode.Int64:
                        writer.WriteNumberValue((long)numericValue);
                        break;
                    case TypeCode.UInt64:
                        writer.WriteNumberValue((ulong)numericValue);
                        break;
                    default:
                        throw new JsonException($"Unsupported enum underlying type: {underlyingType.FullName}.");
                }
            }

            private static TEnum ReadNumber(ref Utf8JsonReader reader)
            {
                var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
                var typeCode = Type.GetTypeCode(underlyingType);

                try
                {
                    object value;
                    switch (typeCode)
                    {
                        case TypeCode.SByte:
                            value = checked((sbyte)reader.GetInt32());
                            break;
                        case TypeCode.Byte:
                            value = checked((byte)reader.GetInt32());
                            break;
                        case TypeCode.Int16:
                            value = checked((short)reader.GetInt32());
                            break;
                        case TypeCode.UInt16:
                            value = checked((ushort)reader.GetInt32());
                            break;
                        case TypeCode.Int32:
                            value = reader.GetInt32();
                            break;
                        case TypeCode.UInt32:
                            value = reader.GetUInt32();
                            break;
                        case TypeCode.Int64:
                            value = reader.GetInt64();
                            break;
                        case TypeCode.UInt64:
                            value = reader.GetUInt64();
                            break;
                        default:
                            throw new JsonException($"Unsupported enum underlying type: {underlyingType.FullName}.");
                    }

                    return (TEnum)Enum.ToObject(typeof(TEnum), value);
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                {
                    throw new JsonException($"The numeric value is outside the range of enum {typeof(TEnum).FullName}.", ex);
                }
            }
        }
    }
}
