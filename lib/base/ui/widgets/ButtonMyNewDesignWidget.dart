//ButtonMyNewDesignWidget.dart
import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:logistits_app/base/ui/widgets/text_widget.dart';

import '../../constants/app_colors.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

class ButtonMyNewDesignWidget extends StatelessWidget {
  const ButtonMyNewDesignWidget(
      {super.key,
        this.height,
        this.width,
        this.borderRadius,
        this.backgroundColor,
        required this.text,
        required this.backgroundassetsimage,
        this.textColor,
        this.textSize,
        this.fontWeight,
        required this.onTap,
        this.svg,
        this.svgWidth,
        this.borderColor});

  final double? height;
  final double? width;
  final double? borderRadius;
  final Color? backgroundColor;
  final String text;
  final String backgroundassetsimage;
  final Color? textColor;
  final double? textSize;
  final FontWeight? fontWeight;
  final Function onTap;
  final String? svg;
  final double? svgWidth;
  final Color? borderColor;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      child: Container(
        height: height ?? kToolbarHeight.h,
        width: width ?? double.infinity,
        decoration: BoxDecoration(
          image: DecorationImage(image: AssetImage('assets/images/$backgroundassetsimage')
              ,fit: BoxFit.fill
          ),
          border: Border.all(
              color: borderColor != null ? borderColor! : Colors.transparent,
              width: borderColor != null ? 1 : 0),
          color: backgroundColor ?? primaryColor,
          borderRadius: BorderRadius.circular(borderRadius ?? 12.r),
        ),
        child: Center(
          child: svg != null
              ? Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              TextWidget(
                text,
                textColor: textColor,
                textSize: textSize,
                fontWeight: fontWeight,
              ),
              5.horizontalSpace,
             Image.asset(
                svg!,
                width: svgWidth,
              )
            ],
          )
              : TextWidget(
            text,
            textColor: textColor,
            textSize: textSize,
            fontWeight: fontWeight,
          ),
        ),
      ),
      onTap: () => onTap(),
    );
  }
}
