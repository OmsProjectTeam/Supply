import 'package:flutter/material.dart';

import '../../../../base/constants/app_colors.dart';
import '../../../../base/constants/app_dimentions.dart';
import '../../../../base/ui/widgets/TextFormFieldWidget.dart';
import '../../../../base/ui/widgets/text_widget.dart';
import '../../registration/ui/signuppage.dart';
class Signin extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return _Signinstate();
  }
}
final scaffoldKey = GlobalKey<ScaffoldState>();
class _Signinstate extends State<Signin>{
  GlobalKey<FormState> globalFormKey = GlobalKey<FormState>();
  late TextEditingController _textFieldController_phone;
  late TextEditingController _textFieldController_password;
  @override
  void initState() {
    super.initState();
    _textFieldController_phone = TextEditingController();
    _textFieldController_password = TextEditingController();
  }

  @override
  void dispose() {
    _textFieldController_phone.dispose();
    _textFieldController_password.dispose();
    super.dispose();
  }
  @override
  Widget build(BuildContext context) {
    var screensize=MediaQuery.of(context).size;

    /// Form ====================================================

    bool iseye=false;
    return Scaffold(
      body: ListView(
        children: [
          Container(
            margin: EdgeInsets.only(top: screensize.height*0.03,bottom: 0),
            child: Center(
              child: Directionality(
                textDirection: TextDirection.rtl,
                child: TextWidget('تسجيل الدخول',
                    fontWeight:  FontWeight.w700,
                    textSize:textverylageLargeSize ,
                ),
              ),
            ),
          ),
          Image.asset('images/logo-png 3.png',
            width: 300,
            height: 200,
          ),
          Stack(
            children: [
              Container(
                height: screensize.height*0.41,
                width: screensize.width*0.99,
                margin: EdgeInsets.only(
                    left: screensize.width*0.04,
                    right: screensize.width*0.04
                ),
                decoration: BoxDecoration(
                    border: Border.all(color: Subthintextcolor),
                    borderRadius: BorderRadius.all(Radius.circular(12))
                ),
                child:  Form(
                  child:Column(
                      children: <Widget>[
                        Container(
                          padding: EdgeInsets.only(top: 10,bottom: 10,right: 5),
                          alignment: Alignment.topRight,
                          margin: EdgeInsets.only(right: 18),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: TextWidget('رقم الهاتف',
                              fontWeight:  FontWeight.w600,
                              textColor: white  ,
                               textSize:  textMediumSize,
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(right: 10,left:10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                color: borders,
                                width:2,
                              )
                          ),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: new
                            TextFormFieldWidget(
                              controller: _textFieldController_phone,
                              label: 'مثال:07503139750',
                              suffixIcon: Icon(
                                Icons.phone,
                                color: Thintextcolor,
                              ),
                              validator: (input) => input!.isEmpty ? "لا يوحد رقم" : null,
                              keyboardType: TextInputType.name,
                              textColor: borders,
                              fontSize: 18,
                              enabledBorderColor: borders,
                              focusedBorderColor: borders,
                            ),
                          ),
                        ),


                        Container(
                          padding: EdgeInsets.only(top: 10,bottom: 10,right: 5),
                          alignment: Alignment.topRight,
                          margin: EdgeInsets.only(right: 18),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: TextWidget('كلمة المرور',
                              fontWeight:  FontWeight.w600,
                              textSize:textMediumSize,
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(right: 5,left: 5),
                          decoration: BoxDecoration(
                              border: Border.all(
                                color: borders,
                                width: 2,
                              )
                          ),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child:TextFormFieldWidget(
                              controller: _textFieldController_phone,
                              textAlign: TextAlign.right,
                              obscure: true,
                              textInputAction: TextInputAction.next,
                              keyboardType: TextInputType.name,
                              onSaved: (input) => _textFieldController_phone.text = input!,
                              validator: (input) => input!.isEmpty ? "لا يوحد كلمة المرور" : null,
                              label: 'كلمة المرور',
                              enabledBorderColor: borders,
                              focusedBorderColor: borders,
                              suffixIcon: InkWell(
                                child: iseye == true
                                    ? Icon(Icons.remove_red_eye_rounded, color: Thintextcolor)
                                    : Image.asset("images/noteye.png"),
                                onTap: () {
                                  setState(() {
                                    iseye = !iseye;
                                    print('iseye =====$iseye');
                                  });
                                },
                              ),
                            ),
                          ),
                        ),
                        SizedBox(height: 35,),
                        Container(
                          margin: EdgeInsets.only(left: 20,right: 5),
                          width: screensize.width*0.99,
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Container(
                                alignment: Alignment.topRight,
                                margin: EdgeInsets.only(right: 30),
                                child: Directionality(
                                  textDirection: TextDirection.rtl,
                                  child: TextWidget('تذكر كلمة المرور',
                                    fontWeight:  FontWeight.w600,
                                    textSize:textSmallSize ,
                                  ),
                                ),
                              ),
                              Container(
                                alignment: Alignment.topRight,
                                margin: EdgeInsets.only(right: 30),
                                child: Directionality(
                                  textDirection: TextDirection.rtl,
                                  child: TextWidget('نسيت كلمة المرور؟',
                                    fontWeight:  FontWeight.w600,
                                    textSize:textSmallSize ,
                                    textColor:Thintextcolor,
                                  ),
                                ),
                              ),

                            ],
                          ),
                        )

                      ]
                  ) ,

                ),
              ),
              Container(
                width: screensize.width*0.99999,
                height: 50,
                margin: EdgeInsets.only(
                    top:  screensize.height*0.39,
                    right: screensize.width*0.15,
                    left:screensize.width*0.15),
                child: ElevatedButton(style: ButtonStyle(
                  backgroundColor: MaterialStateProperty.all(Butn),
                  elevation:MaterialStateProperty.all(12),
                  padding:MaterialStateProperty.all(EdgeInsets.all(5)),
                ), onPressed:(){
                  // Navigator.push(context,
                  //     MaterialPageRoute(builder:  (context) =>mainHomePage()));
                }, child: Directionality(
                  textDirection: TextDirection.rtl,
                  child: TextWidget('تسجيل الدخول',
                    fontWeight:  FontWeight.w600,
                    textSize:textMediumSize,
                  ),
                )),
              ),
            ],
          ),

          SizedBox(height: 90,),
          Container(
            margin:EdgeInsets.only(left: screensize.width*0.20) ,
            child: Center(
                child: Row(
                  children: [
                    InkWell(

                      onTap: (){
                        Navigator.push(context,
                            MaterialPageRoute(builder:  (context) =>signuppage()));
                      },
                      child: Directionality(
                        textDirection: TextDirection.rtl,
                        child: TextWidget('انشئ حسابك الآن',
                          fontWeight:  FontWeight.w900,
                          textSize:textSmallSize2 ,
                          textColor:Thintextcolor,
                        ),
                      ),
                    ),

                    Directionality(
                      textDirection: TextDirection.rtl,
                      child: TextWidget('هل أنت مستخدم جديد؟ ',
                        fontWeight:  FontWeight.w900,
                        textSize:textSmallSize2 ,
                        textColor:Subthintextcolor,
                      ),
                    ),

                  ],
                )
            ),
          ),
        ],
      ),
    );

  }
  Future<bool> validateAndSave() async {
    await Future.delayed(Duration(seconds: 0));

    final form = globalFormKey.currentState;
    if (form!.validate()) {
      form.save();
      return true;
    }
    return false;
  }
  void clearForm() {
    globalFormKey.currentState?.reset();
    _textFieldController_password.clear();
  }

}