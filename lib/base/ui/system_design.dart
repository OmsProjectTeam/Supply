import 'package:flutter/services.dart';

import '../constants/app_colors.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

statusBarsColors({Color color = white}) {
  SystemChrome.setSystemUIOverlayStyle(
    SystemUiOverlayStyle(
      systemNavigationBarColor: color,
      statusBarColor: color,
      statusBarIconBrightness: Brightness.dark,
      systemNavigationBarIconBrightness: Brightness.dark,
    ),
  );
}
