<h1 align="center">CandyChances</h1>
<div align="center">
<a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/github/downloads/MS-crew/CandyChances/total?style=for-the-badge&logo=github" alt="GitHub Release Download"></a>
<a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/badge/Build-2.3.1-brightgreen?style=for-the-badge&logo=gitbook" alt="Latest Release"></a>
<a href="https://github.com/MS-crew/CandyChances/blob/master/LICENSE"><img src="https://img.shields.io/badge/Licence-GPL_3.0-blue?style=for-the-badge&logo=gitbook" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.10.0-green?style=for-the-badge&logo=gitbook" alt="Exiled Dependency"></a>
</div>

<div align="center">
Fine tune every aspect of SCP-330's candy bowl for your server.
</div>

## Features

- **Weighted Candy Chances:** Set individual spawn weights for every candy type in SCP-330.
- **Cooldown & Use Limit Overrides:** Configure a custom global take cooldown and maximum uses per life.
- **Role-Based Limits:** Provide unique use limits for vanilla roles and Exiled custom roles.
- **Dynamic Hint System:** Toggle hints for taked candys from bowl, remaining uses, and hand-sever events with configurable text.
- **Sound Control:** Decide whether players hear the classic candy pickup sound.

## Installation

1. Download the latest release from the [GitHub releases page](https://github.com/MS-crew/CandyChances/releases).
2. Place the extracted files into your `\AppData\Roaming\EXILED\Plugins` directory.
3. Adjust the configuration (see below) to match your server's needs.
4. Restart or reload your server to apply the changes.

## Configuration

CandyChances ships with the following default configuration:

```yml
is_enabled: true
debug: false
# Global Scp330 Bowl settings.
override_candy_take_cooldown: false
modified_candy_take_cooldown: 2
override_global_use_limit: false
modified_global_use_limit: 3
# Modified candy types in SCP-330 bowl.
override_bowl_candys: false
# List of candy types that will appear in SCP-330 bowl.(All Types  )
modified_bowl_candys:
- 'CandyBlue'
- 'CandyPink'
- 'CandyRed'
- 'CandyYellow'
- 'HauntedCandyGray'
- 'HauntedCandyWhite'
- 'HauntedCandyBlack'
- 'HauntedCandyBrown'
- 'HauntedCandyGreen'
- 'HauntedCandyOrange'
- 'HauntedCandyPurple'
- 'HauntedCandyRainbow'
# Modifie candy spawn chances.
override_candy_chances: true
# Modified candy spawn chances in SCP-330 bowl.
candy_chances:
  CandyBlue: 1
  CandyPink: 0.200000003
  CandyRed: 1
  CandyYellow: 1
  HauntedCandyBlack: 1
  HauntedCandyWhite: 1
  HauntedCandyBrown: 1
  HauntedCandyGray: 1
  HauntedCandyGreen: 1
  HauntedCandyOrange: 1
  HauntedCandyPurple: 1
  HauntedCandyRainbow: 1
# Try replicate Hallowen candy behaviors.
try_replicate_halloween_candys: true
# Hint duration and visibility options.
hint_time: 3
hint_position_ruei: 300
show_candy_hint: true
show_remaining_use_hint: true
show_hands_severed_hint: true
should_play_take_sound: true
# Role based SCP-330 Bowl use limits.
override_use_limitsfor_roles: false
modified_use_limits:
  Filmmaker: 99
override_use_limitsfor_custom_roles: false
modified_use_limitsfor_custom_roles:
  Example Role Name: 5
# List of candy types names.
candy_names:
- 'CandyBlue'
- 'CandyGreen'
- 'CandyPink'
- 'CandyPurple'
- 'CandyRainbow'
- 'CandyRed'
- 'CandyYellow'
- 'HauntedCandyBlack'
- 'HauntedCandyBlue'
- 'HauntedCandyBrown'
- 'HauntedCandyEvil'
- 'HauntedCandyGray'
- 'HauntedCandyGreen'
- 'HauntedCandyOrange'
- 'HauntedCandyPink'
- 'HauntedCandyPurple'
- 'HauntedCandyRainbow'
- 'HauntedCandyRed'
- 'HauntedCandyWhite'
- 'HauntedCandyYellow'
```

### Translations

All hint strings can be customised via the translation file, allowing you to provide localized or themed messages for candy pickups and hand-sever events.

## Feedback and Support

- **Report Issues:** Use the [GitHub Issues page](https://github.com/MS-crew/CandyChances/issues).
- **Contact:** [discerrahidenetim@gmail.com](mailto:discerrahidenetim@gmail.com)

Thank you for using CandyChances and helping us improve it!
