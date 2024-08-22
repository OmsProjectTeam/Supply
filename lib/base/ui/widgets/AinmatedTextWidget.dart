import 'dart:async';
import 'package:flutter/material.dart';

import '../../constants/app_colors.dart';

class AnmatedTextWidget extends StatefulWidget {
  const AnmatedTextWidget(
      this.text, {
        super.key,
        this.style,
        this.textSize,
        this.textColor,
        this.fontWeight,
        this.height,
        this.underline = false,
        this.overflow,
        this.maxLine,
        this.textAlign,
        this.duration = const Duration(milliseconds: 300),
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
  final Duration duration;

  @override
  _AnmatedTextWidgetState createState() => _AnmatedTextWidgetState();
}

class _AnmatedTextWidgetState extends State<AnmatedTextWidget> {
  bool _bright = false;
  Timer? _timer;

  @override
  void initState() {
    super.initState();
    _timer = Timer.periodic(Duration(milliseconds: 1500), (timer) {
      setState(() {
        _bright = !_bright;
      });
    });
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedDefaultTextStyle(
      style: widget.style ??
          TextStyle(
            fontSize: widget.textSize,
            color: _bright ? gold2  : widget.textColor ?? grey900,
            fontWeight: widget.fontWeight,
            fontFamily: 'Ubuntu',
            height: widget.height,
            overflow: widget.overflow,
            decoration: widget.underline ? TextDecoration.underline : TextDecoration.none,
          ),
      duration: widget.duration,
      child: Text(
        widget.text,
        maxLines: widget.maxLine,
        textAlign: widget.textAlign,
      ),
    );
  }
}












// import 'package:flutter/material.dart';
//
// /// created by: yazeed nasrullah
// /// email: yazeed.mohm97@gmail.com
//
// class TextWidget extends StatelessWidget {
//   const TextWidget(
//       this.text, {
//         super.key,
//         this.style,
//         this.textSize,
//         this.textColor,
//         this.fontWeight,
//         this.height,
//         this.underline = false,
//         this.overflow,
//         this.maxLine,  this.textAlign
//       });
//
//   final String text;
//   final TextStyle? style;
//   final double? textSize;
//   final Color? textColor;
//   final FontWeight? fontWeight;
//   final double? height;
//   final bool underline;
//   final TextOverflow? overflow;
//   final int? maxLine;
//   final TextAlign? textAlign;
//
//   @override
//   Widget build(BuildContext context) {
//     return Text(
//       text,
//       maxLines: maxLine,
//       textAlign: textAlign,
//       style: style ??
//           TextStyle(
//
//             fontSize: textSize,
//             color: textColor,
//             fontWeight: fontWeight,
//             fontFamily: 'Ubuntu',
//             height: height,
//             overflow: overflow,
//
//             decoration: underline ? TextDecoration.underline : TextDecoration.none,
//           ),
//     );
//   }
// }
