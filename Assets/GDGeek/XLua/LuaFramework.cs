using UnityEngine;
using System.Collections;
/*
using XLua;

public class LuaFramework : MonoBehaviour {
	LuaEnv luaenv_ = null;
	string _gdgeek = @"
		function init()
			local test = CS.UnityEngine.GameObject('test');
			test:AddComponent(typeof(CS.MyTest))
			local mesh = test:AddComponent(typeof(CS.GDGeek.Lua.VoxelMesh))
			mesh:load('a.vox');
			--local mesh = test:GetComponent(typeof(CS.MyTest));

			print(mesh);
			--mytest:cool();
		end

		init()
	
--[[
       --协程下使用
       local co = coroutine.create(function()
           print('------------------------------------------------------')
           demo()
       end)
       assert(coroutine.resume(co))
]]
	";
	void Awake(){


		//luaenv_ = new LuaEnv();
		//luaenv_.DoString(_gdgeek);
	}
	// Update is called once per frame
	void Update () {
		if (luaenv_ != null)
		{
			luaenv_.Tick();
		}
	}

	void OnDestroy()
	{
		if (luaenv_ != null) {
			luaenv_.Dispose ();
		}
	}
}
*/