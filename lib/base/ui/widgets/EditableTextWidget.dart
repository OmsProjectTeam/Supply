import 'package:flutter/material.dart';

class EditableTextWidget extends StatefulWidget {
  const EditableTextWidget({
    super.key,
    required this.text,
    this.textSize,
    this.textColor,
    this.fontWeight,
    this.height,
    this.underline = false,
    this.overflow,
    this.maxLines,
    this.textAlign,
    this.controller,
    this.onChanged,
    this.onEditingComplete,
  });

  final String text;
  final double? textSize;
  final Color? textColor;
  final FontWeight? fontWeight;
  final double? height;
  final bool underline;
  final TextOverflow? overflow;
  final int? maxLines;
  final TextAlign? textAlign;
  final TextEditingController? controller;
  final ValueChanged<String>? onChanged;
  final VoidCallback? onEditingComplete;

  @override
  _EditableTextWidgetState createState() => _EditableTextWidgetState();
}

class _EditableTextWidgetState extends State<EditableTextWidget> {
  late TextEditingController _controller;

  @override
  void initState() {
    super.initState();
    _controller = widget.controller ?? TextEditingController(text: widget.text);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Material(
      color: Colors.transparent,
      child: TextField(
        controller: _controller,
        maxLines: widget.maxLines,
        textAlign: widget.textAlign ?? TextAlign.start,
        style: TextStyle(
          fontSize: widget.textSize,
          color: widget.textColor,
          fontWeight: widget.fontWeight,
          fontFamily: 'Ubuntu',
          height: widget.height,
          overflow: widget.overflow,
          decoration: widget.underline ? TextDecoration.underline : TextDecoration.none,
        ),
        decoration: InputDecoration(
          filled: true,
          fillColor: Colors.transparent,
          border: InputBorder.none,
        ),
        autofocus: true,
        onChanged: widget.onChanged,
        onEditingComplete: widget.onEditingComplete,
      ),
    );
  }
}
