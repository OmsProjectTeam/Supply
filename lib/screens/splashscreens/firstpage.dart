//firstpage
import 'package:flutter/material.dart';
import 'package:logistits_app/screens/splashscreens/secondpage.dart';

import '../../base/constants/app_colors.dart';
import '../../base/constants/app_dimentions.dart';
import '../../base/ui/widgets/loadingButton/LoadingButton.dart';
import '../../base/ui/widgets/text_widget.dart';
class firstpage extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return _firstpagestate();
  }
}
class _firstpagestate extends State<firstpage>{
  @override
  Widget build(BuildContext context) {
    var screensize=MediaQuery.of(context).size;
    return Scaffold(
      body: ListView(
        children: [
          Container(
              margin: EdgeInsets.only(top: screensize.height*0.05,
              ),
              child: Image.asset('images/home1.png')),
          SizedBox(
            height: screensize.height*0.02,
          ),
          Center(
            child:  Directionality(
              textDirection: TextDirection.rtl,
              child: TextWidget('أسهل وأرخص',
                fontWeight:  FontWeight.w700,
                textSize:textLargeSize2 ,

              ),
            ),
          ),
          SizedBox(
            height: screensize.height*0.0012,
          ),
          Container(
            padding: EdgeInsets.only(left: screensize.width*0.18,
                right: screensize.width*0.22,top: 5,bottom: 5),
            child: Center(
              child: Directionality(
                textDirection: TextDirection.rtl,
                child: Align(
                  alignment: Alignment.center,
                  child: TextWidget('استعرض العديد من المنتجات المميزة بسهولة وتمتّع بالكثير من العروض والخصومات الخاصة',
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
          Container(
            alignment: Alignment.center,
            margin: EdgeInsets.only(right: screensize.width*0.45,left: screensize.width*0.45),
            child: Row(
              children: [
                InkWell(
                  child: Image.asset('images/circle2.png'),
                ),
                SizedBox(width: 18,),
                InkWell(
                  child: Image.asset('images/circle1.png'),
                ),
              ],
            ),
          ),
          SizedBox(
            height: screensize.height*0.0255,
          ),
          Container(
            width: 300,
            height: 60,
            decoration: BoxDecoration(
                borderRadius: BorderRadius.all(Radius.circular(25))
            ),
            padding: EdgeInsets.all(3),
            margin: EdgeInsets.only(right: screensize.width*0.25,left:screensize.width*0.25),
            child:
            ElevatedButton(style: ButtonStyle(
              backgroundColor: MaterialStateProperty.all(Butn),
              elevation:MaterialStateProperty.all(3),
              shape: MaterialStateProperty.
              all(ContinuousRectangleBorder(borderRadius:BorderRadius.all(Radius.circular(25)) )),
              padding:MaterialStateProperty.all(EdgeInsets.all(5)),
            ), onPressed:(){
              Navigator.push(context,
                  MaterialPageRoute(builder:  (context) =>  Seconintroui()));
            }, child: Directionality(
              textDirection: TextDirection.rtl,
              child: TextWidget('التالي',
                  textAlign: TextAlign.center,
                  fontWeight:  FontWeight.w400,
                  textSize:textLargeSize3 ,
              ),
            )),
          ),

          SizedBox(
            height: screensize.height*0.039,
          ),
          Container(
            padding: EdgeInsets.only(left: 15,right: 15,top: 0,bottom: 5),
            child: Center(
              child: Directionality(
                textDirection: TextDirection.rtl,
                child:TextWidget('تخطي',
                    fontWeight:  FontWeight.w900,
                    textSize:textSmallSize2 ,
                ),
              ),
            ),
          ),

        ],
      ),
    );
  }

}