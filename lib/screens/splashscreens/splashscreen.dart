import 'package:flutter/material.dart';

import 'firstpage.dart';
class splashscreen extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return _splashscreenstate();
  }
}
class _splashscreenstate extends State<splashscreen>{
  @override
  Widget build(BuildContext context) {
    var screensize=MediaQuery.of(context).size;
    return Scaffold(
      body: SafeArea(
        child: Stack(
          children: [
            Container(
              alignment: Alignment.topCenter,
              margin:EdgeInsets.only(top: screensize.height*0.2,
              ),
              child: InkWell(
                child: Image.asset('images/logo-png 3.png'),
                onTap: (){
                  Navigator.push(context,
                      MaterialPageRoute(builder:  (context) =>  firstpage()));
                },
              ),
            )
          ],
        ),
      ),
    );
  }


}