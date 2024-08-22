import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

import '../../constants/app_images.dart';

class UserTypeWidget extends StatelessWidget {
  final int userType;

  const UserTypeWidget({super.key, required this.userType});

  @override
  Widget build(BuildContext context) {
    switch (userType) {
      case 0:
        return const SizedBox();
      case 807:
        return Image.asset(
          agentImage,
          width: 45.h,
          height: 45.h,
        );
      case 806:
        return Image.asset(
          hostImage,
          width: 45.h,
          height: 45.h,
        );
      default:
        return const SizedBox();
    }
  }
}
