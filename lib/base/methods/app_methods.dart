import 'dart:io';

import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

hideKeyboard(BuildContext context) {
  FocusScope.of(context).requestFocus(FocusNode());
}

Future<File?> pickImage(ImageSource source) async {
  final pickedFile = await ImagePicker().pickImage(source: source);
  if (pickedFile != null) {
    return File(pickedFile.path);
  } else {
    return null;
  }
}

