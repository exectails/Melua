Melua
=============================================================================

Melua is a set of simple .NET Lua 5.1 bindings, to use Lua as it is.
It doesn't provide any wrappers to make usage more idiomatic,
and only adds new functions where usage of the originals is bothersome
from .NET.

I created it because I didn't want any abstraction and bloated libraries,
but simple, raw Lua.

Usage
-----------------------------------------------------------------------------

Google how to do something in C/Lua, prefix the calls with `Melua.`, and
you're good.

```
var L = Melua.luaL_newstate();
Melua.luaL_openlibs(L);
Melua.luaL_dostring(L, "print('Hello, World!");
```

If you're using C# 6+ you can also use a static using to get rid of the
Melua prefix.

```
using static MeluaLib.Melua;

// ...

var L = luaL_newstate();
luaL_openlibs(L);
luaL_dostring(L, "print('Hello, World!");
```
