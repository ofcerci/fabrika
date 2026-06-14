import 'package:flutter/material.dart';
import 'theme_provider.dart';

enum CustomColors {
  baseDefault,
  transparent,
  primary,
  secondary,
  success,
  info,
  warning,
  danger,
  light,
  gray,
  dark,
  white,
}

extension SelectedColorExtension on CustomColors {
  Color get getColor {
    final isDarkMode = ThemeProvider.isDarkMode();

    switch (this) {
      case CustomColors.transparent:
        return Colors.transparent;
      case CustomColors.primary:
        return isDarkMode
            ? const Color(0xFFFF6B35)
            : const Color(0xFFFF5722);
      case CustomColors.secondary:
        return isDarkMode
            ? const Color(0xFF64B5F6)
            : const Color(0xFF1565C0);
      case CustomColors.success:
        return isDarkMode
            ? const Color(0xFF66BB6A)
            : const Color(0xFF2E7D32);
      case CustomColors.info:
        return isDarkMode
            ? const Color(0xFF4FC3F7)
            : const Color(0xFF0277BD);
      case CustomColors.warning:
        return isDarkMode
            ? const Color(0xFFFFCA28)
            : const Color(0xFFF57F17);
      case CustomColors.danger:
        return isDarkMode
            ? const Color(0xFFEF5350)
            : const Color(0xFFC62828);
      case CustomColors.light:
        return const Color(0xFFF5F5F5);
      case CustomColors.dark:
        return isDarkMode
            ? const Color(0xFFFFFFFF)
            : const Color(0xFF0D0D0D);
      case CustomColors.gray:
        return isDarkMode
            ? const Color(0xFF9E9E9E)
            : const Color(0xFF616161);
      case CustomColors.white:
        return const Color(0xFFFFFFFF);
      case CustomColors.baseDefault:
        return const Color(0xFFFF5722);
    }
  }
}
