import 'dart:async';

import 'package:flutter/material.dart';

/// created by: yazeed nasrullah
/// email: yazeed.mohm97@gmail.com

class LoadingButtonProvider extends ChangeNotifier {
  bool _isLoading = false;

  bool get isLoading => _isLoading;

  setLoading(bool value) {
    _isLoading = value;
    notifyListeners();
  }

  ///response
  bool? _success;
  String? _msg;

  bool? get success => _success;

  String? get msg => _msg;

  Future setResponse(bool success, String? msg) async {
    _success = success;
    _msg = msg;

    notifyListeners();

    Timer(const Duration(seconds: 2), () {
      _success = null;
      _msg = null;

      notifyListeners();
    });
  }
}
