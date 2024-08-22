import 'package:flutter/material.dart';

import '../../../../base/constants/app_colors.dart';
import '../../../../base/constants/app_dimentions.dart';
import '../../../../base/ui/widgets/TextFormFieldWidget.dart';
import '../../../../base/ui/widgets/text_widget.dart';
import '../../login/ui/Signin.dart';
class signuppage extends StatefulWidget{
  @override
  State<StatefulWidget> createState() {
    return _signuppagestate();
  }
}

class _signuppagestate extends State<signuppage>{
  GlobalKey<FormState> globalFormKey = GlobalKey<FormState>();
  late TextEditingController _textFieldController_phone;
  late TextEditingController _textFieldController_password;
  bool iseye=false;
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
    return Scaffold(
      body: ListView(
        children: [
          Container(
            margin: EdgeInsets.only(top: screensize.height*0.03,bottom: 0),
            child: Center(
              child: Directionality(
                textDirection: TextDirection.rtl,
                child:TextWidget('إنشاء حساب',
                  fontWeight:  FontWeight.w900,
                  textSize:textverylageLargeSize ,
                  textColor:Thintextcolor,
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
                height: screensize.height*0.79,
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
                              textSize:16 ,
                              textColor:Thintextcolor,
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(right: 10,left: 10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                  color: borders,
                                  width: 2
                              )
                          ),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: new Directionality(
                              textDirection: TextDirection.rtl,
                              child: TextFormFieldWidget(
                                controller: _textFieldController_phone,
                                textAlign: TextAlign.right,
                                textInputAction: TextInputAction.next,
                                keyboardType: TextInputType.name,
                                onSaved: (input) => _textFieldController_phone.text = input!,
                                validator: (input) => input!.isEmpty ? "لا يوحد رقم" : null,
                                enabledBorderColor: borders,
                                focusedBorderColor: borders,
                              ),
                            )
                          ),
                        ),
                        //this is com fiedled
                        Container(
                          padding: EdgeInsets.only(top: 10,bottom: 10,right: 5),
                          alignment: Alignment.topRight,
                          margin: EdgeInsets.only(right: 18),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child:  TextWidget('الاسم',
                              fontWeight:  FontWeight.w600,
                              textSize:16,
                              textColor: Thintextcolor,
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(right: 10,left: 10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                  color: borders,
                                  width: 2
                              )
                          ),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: new TextFormFieldWidget(
                              controller: _textFieldController_phone,
                              textAlign: TextAlign.right,
                              textDirection: TextDirection.rtl,
                              textInputAction: TextInputAction.next,
                              keyboardType: TextInputType.name,
                              onSaved: (input) => _textFieldController_phone.text = input!,
                              validator: (input) => input!.isEmpty ? "الاسم غير صحيح " : null,
                              enabledBorderColor: borders,
                              focusedBorderColor: borders,
                              isBordered: true,
                            ),
                          ),
                        ),
                        //end comp fieled

                        //this is com fiedled 2
                        Container(
                          padding: EdgeInsets.only(top: 10,bottom: 10,right: 5),
                          alignment: Alignment.topRight,
                          margin: EdgeInsets.only(right: 18),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: TextWidget('البلد',
                              fontWeight:  FontWeight.w600,
                              textSize:16,
                              textColor:Thintextcolor,
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.only(right: 10,left: 10),
                          decoration: BoxDecoration(
                              border: Border.all(
                                color: borders,
                                width: 3,
                              )
                          ),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: new TextFormFieldWidget(
                              controller: _textFieldController_phone,
                              textAlign: TextAlign.right,
                              textDirection: TextDirection.rtl,
                              textInputAction: TextInputAction.next,
                              keyboardType: TextInputType.name,
                              onSaved: (input) => _textFieldController_phone.text = input!,
                              validator: (input) => input!.isEmpty ? "لا يوجد كلمة المرور" : null,
                              enabledBorderColor: borders,
                              focusedBorderColor: borders,
                              isBordered: true,
                            ),
                          ),
                        ),
                        //end comp fieled
                        //this is com fiedled
                        Container(
                          padding: EdgeInsets.only(top: 10,bottom: 10,right: 5),
                          alignment: Alignment.topRight,
                          margin: EdgeInsets.only(right: 18),
                          child: Directionality(
                            textDirection: TextDirection.rtl,
                            child: TextWidget('المحافظة',
                              textColor:Thintextcolor,
                             textSize: 16,
                              fontWeight: FontWeight.w600,
                            )
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
                            child: new TextFormFieldWidget(
                              controller: _textFieldController_phone,
                              textAlign: TextAlign.right,
                              textDirection: TextDirection.rtl,
                              textInputAction: TextInputAction.next,
                              keyboardType: TextInputType.name,
                              onSaved: (input) => _textFieldController_phone.text = input!,
                              validator: (input) => input!.isEmpty ? "لا يوجد كلمة المرور" : null,
                              enabledBorderColor: borders,
                              focusedBorderColor: borders,
                              isBordered: true,
                            ),
                          ),
                        ),
                        //end comp fieled

                        //this is com fiedled
                        Container(
                          width: screensize.width*0.99,
                          margin: EdgeInsets.only(
                            left: screensize.width*0.04,
                            right: screensize.width*0.04,
                            //
                          ),
                          child: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            children: [
                              Container(
                                alignment: Alignment.topRight,
                                margin: EdgeInsets.only(right: 5,left: 5),
                                child: Directionality(
                                  textDirection: TextDirection.rtl,
                                  child: InkWell(
                                    onTap: (){
                                      // Navigator.push(context,
                                      //     MaterialPageRoute(builder:  (context) =>Mappage()));
                                    },
                                  child: TextWidget('استخدام الخريطة',
                                  textColor:map,
                                  textSize: 12,
                                  fontWeight: FontWeight.w700,
                                  ),
                                  ),
                                ),
                              ),
                              Container(
                                padding: EdgeInsets.only(top: 5,bottom: 5,right: 5,left: 5),
                                alignment: Alignment.topRight,
                                margin: EdgeInsets.only(right: 5,left: 5),
                                child: Directionality(
                                  textDirection: TextDirection.rtl,
                                  child: TextWidget('العنوان',
                                  textColor: Thintextcolor,
                                  textSize:  16,
                                    fontWeight: FontWeight.w600,

                                  ),
                                ),
                              ),
                            ],
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
                            child: new TextFormField(
                              textAlign: TextAlign.right,
                              textDirection: TextDirection.rtl,
                              textInputAction: TextInputAction.next,
                              style:  TextStyle(
                                color:borders,
                                fontSize: 18,
                                fontFamily: "Cairo",
                                fontWeight: FontWeight.w400,
                              ),
                              keyboardType: TextInputType.name,
                              onSaved: (input) => _textFieldController_phone.text = input!,
                              validator: (input) => input!.isEmpty ? "لا يوحد كلمة المرور" : null,
                              decoration: new InputDecoration(
                                enabledBorder: OutlineInputBorder(
                                    borderSide: BorderSide(color: borders)),
                                focusedBorder: OutlineInputBorder(
                                    borderSide: BorderSide(color: borders)),
                              ),
                            ),
                          ),
                        ),
                        //end comp fieled
                      ]
                  ) ,

                ),
              ),
              Container(
                width: screensize.width*0.99999,
                height: 50,
                margin: EdgeInsets.only(
                    top:  screensize.height*0.76,
                    right: screensize.width*0.15,
                    left:screensize.width*0.15),
                child: ElevatedButton(style: ButtonStyle(
                  backgroundColor: MaterialStateProperty.all(Butn),
                  elevation:MaterialStateProperty.all(12),
                  padding:MaterialStateProperty.all(EdgeInsets.all(5)),
                ), onPressed:(){
                  // Navigator.push(context,
                  //     MaterialPageRoute(builder:  (context) =>  Otp()));


                }, child: Directionality(
                  textDirection: TextDirection.rtl,
                  child: TextWidget('إنشاء الحساب',
           textColor:Colors.white,
                   textSize: 16,
                    fontWeight: FontWeight.w600,
                  ),
                )),
              ),
            ],
          ),

          SizedBox(height: 30,),
          Container(
            margin:EdgeInsets.only(left: screensize.width*0.20) ,
            padding: EdgeInsets.only(left: 15,right: 15,top: 0,bottom: 5),
            child: Center(
                child: Row(
                  children: [
                    InkWell(
                      onTap: (){
                        Navigator.push(context,
                            MaterialPageRoute(builder:  (context) =>Signin()));
                      },
                      child: Directionality(
                        textDirection: TextDirection.rtl,
                        child: TextWidget('قم بتسجيل الدخول',
                          textColor:Thintextcolor,
                      textSize: 13,
                          fontWeight: FontWeight.w900,
                        ),
                      ),
                    ),
                    Directionality(
                      textDirection: TextDirection.rtl,
                      child:TextWidget('هل تملك حساب؟  ',
                        textColor:Subthintextcolor,
                        textSize: 13,
                        fontWeight: FontWeight.w900,
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