import 'package:flutter/material.dart';
import 'package:logistits_app/base/ui/widgets/text_widget.dart';

import '../constants/app_colors.dart';


/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

class AppNotification {
  static showSnackBar(BuildContext context, String msg, {Color bgColor = green}) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: TextWidget(msg, textColor: white), backgroundColor: bgColor),
    );
  }
}
