import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../base/constants/app_colors.dart';
import '../../base/constants/app_dimentions.dart';
import '../../base/ui/widgets/text_widget.dart';
import 'Thirdintro.dart';
class Seconintroui extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return _stateSeconintroui();
  }
}

class _stateSeconintroui extends State<Seconintroui>{
  @override
  Widget build(BuildContext context) {
    var screensize=MediaQuery.of(context).size;
    return Scaffold(
      body: ListView(
        children: [
          Container(
              margin: EdgeInsets.only(top: screensize.height*0.09,
              ),
              child: Image.asset('images/inntrow2.png')),
          SizedBox(
            height: screensize.height*0.04,
          ),
          Center(
            child:  Directionality(
              textDirection: TextDirection.rtl,
              child: TextWidget('توصيل إلى المنزل',
                fontWeight:  FontWeight.w700,
                textSize:textLargeSize2 ,
              ),
            ),
          ),
          SizedBox(
            height: screensize.height*0.0165,
          ),
          Container(
            padding: EdgeInsets.only(left: screensize.width*0.18,
                right: screensize.width*0.22,top: 5,bottom: 5),
            child: Center(
              child: Directionality(
                textDirection: TextDirection.rtl,
                child: Center(
                  child: Align(
                    alignment: Alignment.center, // Align however you like (i.e .centerRight, centerLeft)
                    child:TextWidget('استمتع بخدمات التوصيل حيث تستطيع الطلب ونحن نقوم بتوصيل كافة المنتجات بسرعة وأمانة',
                        textAlign: TextAlign.center,
                        fontWeight:  FontWeight.w400,
                        textSize:textLargeSize3 ,
                    ),
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
            margin: EdgeInsets.only(right: screensize.width*0.45,
                left: screensize.width*0.45),
            child: Row(
              children: [
                InkWell(
                  child: Image.asset('images/circle1.png'),
                ),
                SizedBox(width: 18,),
                InkWell(
                  child: Image.asset('images/circle2.png'),
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
            padding: EdgeInsets.all(3),
            margin: EdgeInsets.only(right: screensize.width*0.25,left:screensize.width*0.25),
            child: ElevatedButton(style: ButtonStyle(
              shape: MaterialStateProperty.
              all(ContinuousRectangleBorder(borderRadius:BorderRadius.all(Radius.circular(25)) )),
              backgroundColor: MaterialStateProperty.all(Butn),
              elevation:MaterialStateProperty.all(3),
              padding:MaterialStateProperty.all(EdgeInsets.all(5)),
            ), onPressed:(){
              Navigator.push(context,
                  MaterialPageRoute(builder:  (context) =>  Thirdintro()));
            }, child: TextWidget('ابدأ الآن',
                fontWeight:  FontWeight.w700,
                textSize:textLargeSize2 ,
            )),
          ),
        ],
      ),
    );
  }

}