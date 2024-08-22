import 'package:flutter/material.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

class NavigationService {
  static GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();

  static pushAndRemoveUntil(BuildContext context, Widget widget) {
    return Navigator.of(context).pushAndRemoveUntil( MaterialPageRoute(builder: (context) => widget), (route) => false);
  }

  static push(BuildContext context, Widget widget) {
    return Navigator.push(context, MaterialPageRoute(builder: (context) => widget));
  }

  static pushReplacement(BuildContext context, Widget widget) {
    return Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => widget));
  }

}
