import 'package:flutter/material.dart';

import '../../constants/app_colors.dart';

class DividerWidget extends StatelessWidget {
  final Color? color;

  const DividerWidget({super.key, this.color});

  @override
  Widget build(BuildContext context) {
    return Divider(
      thickness: 0.3,
      color: color ?? grey500,
    );
  }
}
