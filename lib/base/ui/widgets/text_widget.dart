import 'package:flutter/material.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

class TextWidget extends StatelessWidget {
  const TextWidget(
      this.text, {
        super.key,
        this.style,
        this.textSize,
        this.textColor,
        this.fontWeight,
        this.height,
        this.underline = false,
        this.overflow,
        this.maxLine,  this.textAlign
      });
  final String text;
  final TextStyle? style;
  final double? textSize;
  final Color? textColor;
  final FontWeight? fontWeight;
  final double? height;
  final bool underline;
  final TextOverflow? overflow;
  final int? maxLine;
  final TextAlign? textAlign;

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      maxLines: maxLine,
      textAlign: textAlign,
      style: style ??
          TextStyle(

            fontSize: textSize,
            color: textColor,
            fontWeight: fontWeight,
            fontFamily: 'Ubuntu',
            height: height,
            overflow: overflow,

            decoration: underline ? TextDecoration.underline : TextDecoration.none,
          ),
    );
  }
}













