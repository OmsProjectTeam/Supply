import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:logistits_app/base/ui/widgets/loadingButton/providers/loading_button_provider.dart';
import 'package:provider/provider.dart';

import '../../../constants/app_colors.dart';
import '../animated_text.dart';

class LoadingButton extends StatelessWidget {
  const LoadingButton({
    super.key,
    required this.onTap,
    required this.text,
    this.height,
    this.width,
    this.radius,
    this.margin = EdgeInsets.zero,
    this.padding = EdgeInsets.zero,
    this.bgColor,
    this.textColor,
    this.textSize,
    this.fontWeight,
    this.usedProvider,
    this.enabled = true,
    this.initLoading = true,
  });

  final Function() onTap;
  final double? height;
  final double? width;
  final double? radius;
  final EdgeInsets? margin;
  final EdgeInsets? padding;
  final Color? bgColor;
  final String text;
  final Color? textColor;
  final double? textSize;
  final FontWeight? fontWeight;
  final Function(LoadingButtonProvider provider)? usedProvider;
  final bool enabled;
  final bool initLoading;

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
      create: (context) => LoadingButtonProvider(),
      child: Consumer<LoadingButtonProvider>(
        builder: (context, provider, child) {
          if (usedProvider != null) usedProvider!(provider);
          return Container(
            height: height ?? kToolbarHeight.h,
            margin: margin,
            padding: padding,
            child: GestureDetector(
              onTap: !enabled
                  ? null
                  : provider.isLoading
                  ? null
                  : () {
                if (initLoading)
                  context
                      .read<LoadingButtonProvider>()
                      .setLoading(!provider.isLoading);
                onTap.call();
              },
              child: AnimatedContainer(
                duration: const Duration(milliseconds: 150),
                height: height ?? kToolbarHeight.h,
                width: provider.isLoading
                    ? kToolbarHeight.h
                    : width ?? MediaQuery.sizeOf(context).width,
                decoration: BoxDecoration(
                  color: provider.success == null
                      ? bgColor ?? primaryColor
                      : provider.success == true
                      ? primaryColor
                      : red,
                  borderRadius: BorderRadius.circular(
                      provider!.isLoading ? 50 : radius ?? 20),
                ),
                child: Center(
                  child: provider.isLoading
                      ? const CircularProgressIndicator(color: white)
                      : AnimatedTextWidget(
                    text: provider.msg ?? text,
                    fontWeight: fontWeight ?? FontWeight.w500,
                    textSize: textSize ?? 18.sp,
                    textColor: textColor ?? white,
                  ),
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}
