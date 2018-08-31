using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    public static class Register
    {
        public static Dictionary<string, Type> NeuralNodeRegisterCollection = new Dictionary<string, Type>();

        public static void RegisterNeuralNode(string name, Type type)
        {
            NeuralNodeRegisterCollection[name] = type;
        }
    }
}
