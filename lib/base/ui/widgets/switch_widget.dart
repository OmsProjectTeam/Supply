import 'package:flutter/material.dart';

import '../../constants/app_colors.dart';

class SwitchWidget extends StatelessWidget {
  final bool switchCheck;

  const SwitchWidget({super.key, required this.switchCheck});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
        child: Transform.scale(
      scale: 0.8, // Adjust the scale factor as needed
      child: Theme(
        data: ThemeData(
          useMaterial3: true,
        ).copyWith(
          colorScheme: Theme.of(context).colorScheme.copyWith(outline: grey300),
        ),
        child: Switch(
          value: switchCheck,
          onChanged: (bool newValue) {
            // Handle the onChanged event
          },
          inactiveThumbColor: white,
          inactiveTrackColor: grey300,
        ),
      ),
    ));
  }
}
