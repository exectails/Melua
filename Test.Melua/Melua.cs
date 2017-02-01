// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace MeluaLib.Test
{
	// A few tests for potential issues that came up in the past.
	public class MeuaTests
	{
		[Fact]
		public void tostring()
		{
			var L = Melua.luaL_newstate();

			Melua.lua_pushinteger(L, 123);
			var str = Melua.lua_tostring(L, -1);
			Assert.Equal("123", str);

			Melua.lua_newtable(L);
			Assert.Equal(null, Melua.lua_tostring(L, -1));

			Melua.lua_pushstring(L, "foobar");
			Assert.Equal("foobar", Melua.lua_tostring(L, -1));
		}

		[Fact]
		public void type()
		{
			var L = Melua.luaL_newstate();

			Melua.lua_pushinteger(L, 123);
			Assert.Equal(Melua.LUA_TNUMBER, Melua.lua_type(L, -1));

			Melua.lua_pushstring(L, "123");
			Assert.Equal(Melua.LUA_TSTRING, Melua.lua_type(L, -1));

			Melua.lua_newtable(L);
			Assert.Equal(Melua.LUA_TTABLE, Melua.lua_type(L, -1));
		}

		[Fact]
		public void typename()
		{
			var L = Melua.luaL_newstate();

			Melua.lua_pushinteger(L, 123);
			Assert.Equal("number", Melua.luaL_typename(L, -1));

			Melua.lua_pushstring(L, "123");
			Assert.Equal("string", Melua.luaL_typename(L, -1));

			Melua.lua_newtable(L);
			Assert.Equal("table", Melua.luaL_typename(L, -1));
		}

		[Fact]
		public void tonumber()
		{
			var L = Melua.luaL_newstate();

			Melua.lua_pushstring(L, "123");
			Assert.Equal(123, Melua.lua_tonumber(L, -1));

			Melua.lua_pushstring(L, "0x123");
			Assert.Equal(0x123, Melua.lua_tonumber(L, -1));

			Melua.lua_pushstring(L, "0x12AB34");
			Assert.Equal(0x12AB34, Melua.lua_tonumber(L, -1));
		}

		[Fact]
		public void tocfunction()
		{
			var L = Melua.luaL_newstate();
			var n = 0;

			Melua.melua_register(L, "foo", _ => { n += 1; return 0; });
			Melua.melua_register(L, "bar", _ => { var func = Melua.lua_tocfunction(L, 1); func(L); return 0; });

			Melua.luaL_dostring(L, "foo()");
			Assert.Equal(1, n);

			Melua.luaL_dostring(L, "bar(foo)");
			Assert.Equal(2, n);
		}

		[Fact]
		public void userdata()
		{
			var L = Melua.luaL_newstate();
			Melua.melua_opensafelibs(L);

			var n1 = 0;

			// Ctor
			Melua.luaL_register(L, "Test", new[]
			{
				new MeluaLib.Melua.LuaLib("new", NL =>
				{
					var test = new UserDataTest() { N1 = 1234 };
					var size = Marshal.SizeOf(test);

					var ptr = Melua.lua_newuserdata(L, size);
					Melua.luaL_getmetatable(L, "Melua.Test");
					Melua.lua_setmetatable(L, -2);

					Marshal.StructureToPtr(test, ptr, true);

					return 1;
				})
			});

			// Meta table for test userdata type
			Melua.luaL_newmetatable(L, "Melua.Test");
			Melua.lua_pushstring(L, "__index");
			Melua.lua_pushvalue(L, -2);
			Melua.lua_settable(L, -3);

			Melua.luaL_register(L, null, new[]
			{
				new MeluaLib.Melua.LuaLib("setN1", _ =>
				{
					var ptr = Melua.luaL_checkudata(L, 1, "Melua.Test");
					var val = Melua.luaL_checkinteger(L,2);

					// Either marshal back and forth or use unsafe
					var test = (UserDataTest)Marshal.PtrToStructure(ptr, typeof(UserDataTest));
					test.N1 = val;
					Marshal.StructureToPtr(test, ptr, true);

					//unsafe
					//{
					//	var test = (UserDataTest*)ptr;
					//	test->N1 = val;
					//}

					return 0;
				}),
				new MeluaLib.Melua.LuaLib("getN1", _ =>
				{
					var ptr = Melua.luaL_checkudata(L, 1, "Melua.Test");
					var test = (UserDataTest)Marshal.PtrToStructure(ptr, typeof(UserDataTest));

					Melua.lua_pushinteger(L, test.N1);

					return 1;
				})
			});

			// Test method
			Melua.melua_register(L, "testgetn1", _ =>
			{
				n1 = Melua.lua_tointeger(L, -1);

				return 0;
			});

			var result = Melua.luaL_dostring(L, @"
local t = Test.new()
t:setN1(5678)
testgetn1(t:getN1())
");

			if (result != 0)
				throw new Exception(Melua.lua_tostring(L, -1));

			Assert.Equal(n1, 5678);
		}

		private struct UserDataTest
		{
			public int N1;
		}
	}
}
