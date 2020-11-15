mergeInto(LibraryManager.library, {
	SyncFiles : function()
	{
		FS.syncfs(false, function(err)
		{
			console.log("Web Assembly: synced to file system's local source");
		});
	}
});