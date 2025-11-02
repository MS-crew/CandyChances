<h1 align="center">CandyChances</h1>
<div align="center">
<a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/github/downloads/MS-crew/CandyChances/total?style=for-the-badge&logo=github" alt="GitHub Release Download"></a>
<a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/badge/Build-2.3.0-brightgreen?style=for-the-badge&logo=gitbook" alt="Latest Release"></a>
<a href="https://github.com/MS-crew/CandyChances/blob/master/LICENSE"><img src="https://img.shields.io/badge/Licence-GPL_3.0-blue?style=for-the-badge&logo=gitbook" alt="General Public License v3.0"></a>
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.0.0-green?style=for-the-badge&logo=gitbook" alt="Exiled Dependency"></a>
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

override_candy_take_cooldown: false
modified_candy_take_cooldown: 2

override_global_use_limit: false
modified_global_use_limit: 3

candy_chances:
  Blue: 15
  Green: 15
  Pink: 10
  Purple: 15
  Rainbow: 15
  Red: 15
  Yellow: 15

hint_time: 3
show_candy_hint: true
show_remaining_use_hint: true
show_hands_severed_hint: true
should_play_take_sound: true

override_use_limitsfor_roles: false
modified_use_limits:
  Tutorial: 99

override_use_limitsfor_custom_roles: false
modified_use_limitsfor_custom_roles:
  Example Role Name: 5
```

### Translations

All hint strings can be customised via the translation file, allowing you to provide localized or themed messages for candy pickups and hand-sever events.

## Feedback and Support

- **Report Issues:** Use the [GitHub Issues page](https://github.com/MS-crew/CandyChances/issues).
- **Contact:** [discerrahidenetim@gmail.com](mailto:discerrahidenetim@gmail.com)

Thank you for using CandyChances and helping us improve it!
