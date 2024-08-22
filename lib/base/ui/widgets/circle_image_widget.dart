import 'package:flutter/material.dart';

import '../../constants/app_colors.dart';



class CircleImageWidget extends StatelessWidget {
  final double radius;
  final String urlImage;
  final Color? borderColor;
  final double? borderWidth;

  const CircleImageWidget(
      {super.key, required this.radius, required this.urlImage, this.borderColor, this.borderWidth});

  @override
  Widget build(BuildContext context) {
    return Container(
      width: radius,
      height: radius,
      decoration: BoxDecoration(
          shape: BoxShape.circle,
          border: Border.all(color:borderColor??transparent ,width: borderWidth??1),
          image: DecorationImage(image: AssetImage(urlImage), fit: BoxFit.cover)),
    );
  }
}
