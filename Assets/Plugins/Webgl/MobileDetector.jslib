var DetectDevice = {
  _isMobile: function () {
    return Module.SystemInfo.mobile;
  },

  closewindow: function () {
    window.close();
  },
};

mergeInto(LibraryManager.library, DetectDevice);
