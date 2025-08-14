<h1 align="center">CandyChances</h1> 
<div align="center">
<a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/github/downloads/MS-crew/CandyChances/total?style=for-the-badge&logo=githubactions&label=Downloads" alt="GitHub Release Download"></a> <a href="https://github.com/MS-crew/CandyChances/releases"><img src="https://img.shields.io/badge/Build-2.1.0-brightgreen?style=for-the-badge&logo=gitbook" alt="GitHub Releases"></a> 
<a href="https://github.com/MS-crew/CandyChances/blob/master/LICENSE">
<img src="https://img.shields.io/badge/Licence-GPL_3.0-blue?style=for-the-badge&logo=gitbook" alt="General Public License v3.0"></a> 
<a href="https://github.com/ExMod-Team/EXILED"><img src="https://img.shields.io/badge/Exiled-9.0.0-greeen?style=for-the-badge&logo=gitbook" alt="GitHub Exiled"></a> 
</div>


<div align="center">
You can change the chances of all the candies coming out</div>


## CandyChances

- **Customizable Chances:** You can adjust the drop chances for each type of candy individually, allowing you to control how likely each one is to appear.
- **Customizable Hints:** You can customize the hint messages shown when a candy is taken or when a player's hands are severed.
- **Customizable Usage Counts:** You can set how many times a player can take candy before their hands are severed, including different limits for custom roles.

1. Download the release file from the GitHub page [here](https://github.com/MS-crew/CandyChances/releases).
2. Extract the contents into your `\AppData\Roaming\EXILED\Plugins` directory.
3. Configure the plugin according to your server’s needs using the provided settings.
4. Restart your server to apply the changes.

## Feedback and Issues

This is the initial release of the plugin. We welcome any feedback, bug reports, or suggestions for improvements.

- **Report Issues:** [Issues Page](https://github.com/MS-crew/CandyChances/issues)
- **Contact:** [discerrahidenetim@gmail.com](mailto:discerrahidenetim@gmail.com)

Thank you for using our plugin and helping us improve it!

Default Config
```yml
is_enabled: true
debug: false
should_play_take_sound: true
hands_severed_hint: true
show_remaining_use: false
hands_severed_hint_time: 3
candy_hint_time: 3
candy_chances:
  Blue: 15
  Green: 15
  Pink: 10
  Purple: 15
  Rainbow: 15
  Red: 15
  Yellow: 15
modified_use_limits:
  ClassD: 3
  NtfCaptain: 5
modified_use_limitsfor_custom_roles:
  Candy Monster: 99
  Chaos Bomber: 10
```
