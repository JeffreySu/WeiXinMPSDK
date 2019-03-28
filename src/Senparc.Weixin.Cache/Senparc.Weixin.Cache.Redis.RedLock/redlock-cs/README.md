redlock-cs
==========

[![Build status](https://ci.appveyor.com/api/projects/status/xat1stsmpvl3gjcg?svg=true)](https://ci.appveyor.com/project/KidFashion/redlock-cs)

Distributed lock with Redis and C# (based on the [redlock algorithm](http://redis.io/topics/distlock))

Redlock-cs is available through nuget as [redlock-cs package](http://www.nuget.org/packages/redlock-cs/).

## Usage

Check our [Unit Test](https://github.com/KidFashion/redlock-cs/blob/master/tests/MultiServerLockTests.cs).

The API is based on antirez [Ruby implementation](https://github.com/antirez/redlock-rb) and works as in the following example:

```csharp
// Declare a Distributed Lock based on 3 REDIS servers

var dlm = new Redlock(
		ConnectionMultiplexer.Connect("127.0.0.1:6379"), 
		ConnectionMultiplexer.Connect("127.0.0.1:6380"), 
		ConnectionMultiplexer.Connect("127.0.0.1:6381")
	      );

// Declare lock object.
Lock lockObject;

// Try to acquire the lock (with resourceName as lock identifier and an 
// expiration time of 10 seconds).
var locked = dlm.Lock(
		resourceName,
		new TimeSpan(0, 0, 10), 
		out lockObject
	     );

// If locked is true, lockObject is populated and the lock has been acquired, 
// otherwise the lock has not been acquired.

// Tries to release the lock contained in lockObject.
dlm.Unlock(lockObject);
```

## TODO

* Disposable pattern. 
* Hide StackExchange.Redis library inside Redlock object.
