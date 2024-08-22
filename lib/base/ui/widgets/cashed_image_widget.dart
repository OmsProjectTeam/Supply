import 'package:cached_network_image/cached_network_image.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';

import '../../constants/app_icons.dart';
class CashedImageWidget extends StatelessWidget {
  final String image;
  final double height;
  final double width;
  final BoxFit fit;
  const CashedImageWidget({super.key, required this.image, required this.height, required this.width, required this.fit});

  @override
  Widget build(BuildContext context) {
    return image == ''
        ? Center(
      child: SvgPicture.asset(
        icDefaultPerson,
        height: height / 2,
        width: width / 2,
        fit: fit,
      ),
    )
        : CachedNetworkImage(
          imageUrl: image,
          errorWidget: (context, url, error) => SvgPicture.asset(icDefaultPerson),
          height: height,
          width: width,
          fit: fit,
        );
  }
}
