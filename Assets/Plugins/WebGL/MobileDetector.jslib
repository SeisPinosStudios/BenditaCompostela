var MobileDetector = {
	IsMobile: function()
	{
		return Module.SystemInfo.mobile
	}
};

mergeInto(LibraryManager.library, MobileDetector);