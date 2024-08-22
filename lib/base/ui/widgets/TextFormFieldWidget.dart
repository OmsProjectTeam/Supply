import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class TextFormFieldWidget extends StatelessWidget {
  const TextFormFieldWidget({
    Key? key,
    required this.controller,
    this.label,
    this.prefixIcon,
    this.validator,
    this.keyboardType,
    this.inputFormatters,
    this.prefixIconConstraints,
    this.enabled = true,
    this.isDense = true,
    this.margin,
    this.backgroundColor,
    this.borderColor,
    this.enabledBorderColor,
    this.focusedBorderColor,
    this.maxLength,
    this.borderRadius,
    this.suffixIcon,
    this.suffixIconConstraints,
    this.hintText,
    this.isBordered = true,
    this.maxLines,
    this.minLines,
    this.hintSize,
    this.hintColor,
    this.contentPaddingVertical,
    this.contentPaddingHorizontal,
    this.fontSize,
    this.readOnly,
    this.textColor,
    this.obscure,
    this.textAlign,
    this.textInputAction,
    this.onSaved,
    this.onChanged,
    this.textDirection, // Add this
  }) : super(key: key);

  final TextEditingController controller;
  final String? label;
  final Widget? prefixIcon;
  final Widget? suffixIcon;
  final String? Function(String?)? validator;
  final TextInputType? keyboardType;
  final List<TextInputFormatter>? inputFormatters;
  final BoxConstraints? prefixIconConstraints;
  final BoxConstraints? suffixIconConstraints;
  final bool enabled;
  final bool isDense;
  final EdgeInsetsGeometry? margin;
  final Color? backgroundColor;
  final Color? borderColor;
  final Color? enabledBorderColor;
  final Color? focusedBorderColor;
  final int? maxLength;
  final double? borderRadius;
  final String? hintText;
  final bool? isBordered;
  final int? maxLines;
  final int? minLines;
  final double? hintSize;
  final double? fontSize;
  final double? contentPaddingVertical;
  final double? contentPaddingHorizontal;
  final Color? hintColor;
  final Color? textColor;
  final bool? readOnly;
  final bool? obscure;
  final TextAlign? textAlign;
  final TextInputAction? textInputAction;
  final void Function(String?)? onSaved;
  final Function(String)? onChanged;
  final TextDirection? textDirection; // Add this

  @override
  Widget build(BuildContext context) {
    return Container(
      margin: margin,
      child: TextFormField(
        controller: controller,
        textDirection: textDirection, // Use this
        textInputAction: textInputAction,
        style: TextStyle(
          color: textColor ?? Colors.black,
          fontFamily: 'Ubuntu',
          fontSize: fontSize ?? 18,
          fontWeight: FontWeight.w400,
        ),
        textAlign: textAlign ?? TextAlign.start,
        keyboardType: keyboardType ?? TextInputType.name,
        onSaved: onSaved,
        validator: validator,
        obscureText: obscure ?? false,
        readOnly: readOnly ?? false,
        enabled: enabled,
        inputFormatters: inputFormatters,
        maxLength: maxLength,
        maxLines: maxLines ?? 1,
        minLines: minLines ?? 1,
        decoration: InputDecoration(
          labelText: label,
          hintText: hintText,
          hintStyle: TextStyle(
            color: hintColor ?? Colors.grey,
            fontSize: hintSize ?? 16,
          ),
          enabledBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(borderRadius ?? 8.0),
            borderSide: BorderSide(
              color: enabledBorderColor ?? borderColor ?? Colors.grey,
            ),
          ),
          focusedBorder: OutlineInputBorder(
            borderRadius: BorderRadius.circular(borderRadius ?? 8.0),
            borderSide: BorderSide(
              color: focusedBorderColor ?? borderColor ?? Colors.blue,
            ),
          ),
          suffixIcon: suffixIcon,
          suffixIconConstraints: suffixIconConstraints,
          prefixIcon: prefixIcon,
          prefixIconConstraints: prefixIconConstraints,
          contentPadding: EdgeInsets.symmetric(
            vertical: contentPaddingVertical ?? 16.0,
            horizontal: contentPaddingHorizontal ?? 16.0,
          ),
          isDense: isDense,
          fillColor: backgroundColor,
          filled: backgroundColor != null,
        ),
        onChanged: onChanged,
      ),
    );
  }
}
