var For7DGame = {
    Loaded: function(){
         if(dgame){
            dgame.loaded();
        }
    }/*,
    Hello: function()
    {
        window.alert("Hello, world!");
		SendMessage ('GameObject', 'ForTest', 'foobar');
    },
    HelloString: function(str)
    {
        window.alert(Pointer_stringify(str));
    },
    PrintFloatArray: function(array, size)
    {
        for(var i=0;i<size;i++)
            console.log(HEAPF32[(array>>2)+size]);
    },
    AddNumbers: function(x,y)
    {
        return x + y;
    },
    StringReturnValueFunction: function()
    {
        var returnStr = "bla";
        var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
        writeStringToMemory(returnStr, buffer);
        return buffer;
    },
    BindWebGLTexture: function(texture)
    {
        GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
    }*/
};

mergeInto(LibraryManager.library, For7DGame);