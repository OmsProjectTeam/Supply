import 'package:flutter/material.dart';

import '../../base/constants/app_colors.dart';
import '../../base/constants/app_dimentions.dart';
import '../../base/ui/widgets/text_widget.dart';
import '../login_regs_pages/login/ui/Signin.dart';
class Thirdintro extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return _thirdintro();
  }
}
class _thirdintro extends State<Thirdintro>{
  @override
  Widget build(BuildContext context) {
    var screensize=MediaQuery.of(context).size;
    return Scaffold(
      body: ListView(
        children: [
          Container(
              margin: EdgeInsets.only(top: screensize.height*0.05,
              ),
              child: Image.asset('images/into3.png')),
          SizedBox(
            height: screensize.height*0.0255,
          ),
          Center(
            child: Directionality(
              textDirection: TextDirection.rtl,
              child: TextWidget('إذن الوصول',
                fontWeight:  FontWeight.w700,
                textSize:textLargeSize2 ,
              ),
            ),
          ),
          SizedBox(
            height: screensize.height*0.0255,
          ),
          Container(
            padding: EdgeInsets.only(left: screensize.width*0.18,
                right: screensize.width*0.22,top: 5,bottom: 5),
            child: Center(
              child:Directionality(
                textDirection: TextDirection.rtl,
                child: Align(
                  alignment: Alignment.center, // Align however you like (i.e .centerRight, centerLeft)
                  child: TextWidget('يحتاج التطبيق إلى صلاحية الوصول إلى موقعك من أجل خدمات التوصيل',
                    textAlign: TextAlign.center,
                    fontWeight:  FontWeight.w400,
                    textSize:textLargeSize3 ,
                  ),
                ),
              ),
            ),
          ),

          SizedBox(
            height: screensize.height*0.0255,
          ),
          SizedBox(
            height: screensize.height*0.0155,
          ),
          Container(
            width: 300,
            height: 60,
            margin: EdgeInsets.only(right: screensize.width*0.25,left:screensize.width*0.25),
            child: ElevatedButton(style: ButtonStyle(
              shape: MaterialStateProperty.
              all(ContinuousRectangleBorder(borderRadius:BorderRadius.all(Radius.circular(25)) )),
              backgroundColor: MaterialStateProperty.all(Butn),
              elevation:MaterialStateProperty.all(3),
              padding:MaterialStateProperty.all(EdgeInsets.all(5)),
            ), onPressed:(){
              Navigator.push(context,
                  MaterialPageRoute(builder:  (context) =>  Signin()));
            }, child: TextWidget('السماح',
              fontWeight:  FontWeight.w700,
              textSize:textLargeSize2 ,
            )),
          ),
          SizedBox(
            height: screensize.height*0.0255,
          ),
          Container(
            padding: EdgeInsets.only(left: 15,right: 15,top: 0,bottom: 5),
            child: Center(
              child:  Directionality(
                textDirection: TextDirection.rtl,
                child: Align(
                  alignment: Alignment.center, // Align however you like (i.e .centerRight, centerLeft)
                  child: TextWidget('الرفض',
                      textAlign: TextAlign.center,
                      fontWeight:  FontWeight.w400,
                      textSize:textMediumSize ,
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

