import 'package:flutter/material.dart';
import 'package:logistits_app/screens/splashscreens/splashscreen.dart';
import 'package:provider/provider.dart';

import 'base/ui/widgets/loadingButton/providers/loading_button_provider.dart';
void main() {
  runApp(const MyApp());
}
class MyApp extends StatelessWidget {
  const MyApp({super.key});
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (ctx) => LoadingButtonProvider()),
      ],
      child: MaterialApp(
        title: 'logistits_app',
        theme: ThemeData(
          primarySwatch: Colors.blue,
        ),
        home:splashscreen(),
        debugShowCheckedModeBanner: false,
        //splashscreen(),Home(),mainHomePage()
      ),
    );
  }
}



