import 'package:flutter/cupertino.dart';

class UserImageProvider extends ChangeNotifier{
  bool _isLoading = true;

  bool get isLoading => _isLoading;

  setLoading(bool value) {
    _isLoading = value;
    notifyListeners();
  }
}