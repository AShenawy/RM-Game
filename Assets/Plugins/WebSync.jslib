mergeInto(LibraryManager.library, {

	SyncFiles : function()
	{
		FS.syncfs(false, function(err)
		{
			console.log("Web Assembly: synced to file system's local source");
		});
	},
	
	OpenURLNewWindow : function(url)
	{
		window.open(Pointer_stringify(url), '_blank');
	},
});