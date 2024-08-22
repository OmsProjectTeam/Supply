import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

import '../../constants/app_colors.dart';
class GreySpace extends StatelessWidget {
  const GreySpace({super.key});

  @override
  Widget build(BuildContext context) {
    return   Container(
      width: MediaQuery.of(context).size.width,
      height: 20.h,
      color: grey300,
    );
  }
}
