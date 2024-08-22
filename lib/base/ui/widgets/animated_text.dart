import 'package:flutter/material.dart';

import 'text_widget.dart';
class AnimatedTextWidget extends StatefulWidget {
  final String text;
  final FontWeight fontWeight;
  final double textSize;
  final Color textColor;

  const AnimatedTextWidget({
    super.key,
    required this.text,
    required this.fontWeight,
    required this.textSize,
    required this.textColor,
  });

  @override
  _AnimatedTextWidgetState createState() => _AnimatedTextWidgetState();
}

class _AnimatedTextWidgetState extends State<AnimatedTextWidget>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _animation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 500),
    );
    _animation = CurvedAnimation(parent: _controller, curve: Curves.easeInOut);
    _controller.forward();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _animation,
      builder: (context, child) {
        return Opacity(
          opacity: _animation.value,
          child: Transform.scale(
            scale: _animation.value,
            child: TextWidget(
              widget.text,
              fontWeight: widget.fontWeight,
              textSize: widget.textSize,
              textColor: widget.textColor,
            ),
          ),
        );
      },
    );
  }
}
