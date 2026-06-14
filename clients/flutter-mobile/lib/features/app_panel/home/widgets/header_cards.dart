import 'package:flutter/material.dart';
import '../../../../core/theme/custom_colors.dart';
import '../../../../core/widgets/base_widgets.dart';
import '../../../../core/theme/extensions.dart';

Padding buildActiveMembersCard(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
      context,
      Icons.people_alt_outlined,
      color: CustomColors.primary.getColor,
      "248",
      "Aktif Üye",
      footer: "Bu ay +12 yeni üye",
    ),
  );
}

Padding buildDailyCheckInsCard(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
      context,
      Icons.login_outlined,
      color: CustomColors.success.getColor,
      "64",
      "Bugünkü Giriş",
      footer: "Son güncelleme: az önce",
    ),
  );
}

Padding buildMonthlyRevenueCard(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
      context,
      Icons.payments_outlined,
      color: CustomColors.warning.getColor,
      "₺47.850",
      "Aylık Gelir",
      footer: "Geçen aya göre +%8",
    ),
  );
}

Padding buildActiveLessonsCard(BuildContext context) {
  return Padding(
    padding: context.defaultPadding,
    child: buildInfoCardWithIconAndFooter(
      context,
      Icons.fitness_center_outlined,
      color: CustomColors.secondary.getColor,
      "12",
      "Aktif Ders",
      footer: "3 ders bugün başlıyor",
    ),
  );
}
