# 🏀 VR Basketball Game

> Trò chơi bóng rổ thực tế ảo (VR) được phát triển trên Unity 2022.3.60f1 với XR Interaction Toolkit

[![Unity](https://img.shields.io/badge/Unity-2022.3.60f1-black.svg?style=flat&logo=unity)](https://unity.com/)
[![XR Toolkit](https://img.shields.io/badge/XR%20Toolkit-3.1.2-blue.svg)](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.1/manual/index.html)
[![Platform](https://img.shields.io/badge/Platform-VR-orange.svg)](https://www.meta.com/quest/)

## 📖 Giới Thiệu

VR Basketball Game là một trò chơi bóng rổ thực tế ảo cho phép người chơi trải nghiệm cảm giác ném bóng vào rổ một cách chân thực trong môi trường VR. Game hỗ trợ các thiết bị VR phổ biến như Meta Quest, HTC Vive, và Valve Index.

### ✨ Tính Năng Chính

- 🎯 **Physics-Based Throwing** - Hệ thống ném bóng dựa trên vật lý thực tế
- 🎮 **VR Interaction** - Tương tác tự nhiên với XR Grab Interactable
- 📊 **Score System** - Hệ thống điểm số với high score persistence
- 🔄 **Auto Ball Reset** - Tự động reset bóng sau khi ghi điểm
- 🌟 **Realistic Ball Physics** - Vật lý bóng rổ chân thực (khối lượng, drag, bounce)
- 💾 **Data Persistence** - Lưu điểm cao nhất bằng PlayerPrefs

## 🎮 Gameplay

1. **Nhặt bóng**: Sử dụng nút Grip trên VR controller
2. **Ngắm**: Hướng tay về phía rổ bóng rổ
3. **Ném**: Vung tay và thả nút Grip để ném bóng
4. **Ghi điểm**: Bóng đi qua rổ sẽ được tính điểm
5. **Lặp lại**: Bóng tự động reset về vị trí ban đầu

## 🛠️ Công Nghệ Sử Dụng

### Unity Packages
- **Unity XR Interaction Toolkit** (v3.1.2) - VR interaction system
- **Unity Input System** (v1.13.1) - Modern input handling
- **TextMeshPro** (v3.0.9) - UI text rendering
- **Universal Render Pipeline** (v14.0.12) - Graphics rendering
- **XR Plugin Management** (v4.5.1) - XR platform management
- **OpenXR** (v1.14.1) - Cross-platform VR support

### Architecture & Patterns
- **Singleton Pattern** - ScoreManager global instance
- **Observer Pattern** - Event-driven VR interactions
- **Component Pattern** - Modular Unity components
- **Coroutines** - Asynchronous score/reset logic

## 📂 Cấu Trúc Dự Án

```
Assets/
├── Scenes/
│   └── New Folder/
│       ├── New Scene.unity              # Scene chính
│       ├── BasketballController.cs      # Điều khiển bóng rổ
│       ├── BasketballHoop.cs            # Quản lý rổ và ghi điểm
│       ├── ScoreManager.cs              # Quản lý điểm số (Singleton)
│       ├── BallRespawner.cs             # Spawn và respawn bóng
│       ├── README_SETUP.md              # Hướng dẫn setup chi tiết
│       └── LUONG_HOAT_DONG.md          # Tài liệu luồng hoạt động
├── Prefabs/                             # Prefabs (Basketball, etc.)
├── Materials/                           # Vật liệu (mat_brown, mat_red, etc.)
└── Settings/                            # Project settings
```

## 🚀 Cài Đặt và Chạy

### Yêu Cầu Hệ Thống

- **Unity:** 2022.3.60f1 hoặc cao hơn
- **Platform:** Windows 10/11 (PC VR) hoặc Android (standalone VR)
- **VR Headset:** Meta Quest 2/3/Pro, HTC Vive, Valve Index, hoặc tương thích OpenXR
- **RAM:** Tối thiểu 8GB (khuyến nghị 16GB)
- **GPU:** NVIDIA GTX 1060 / AMD RX 580 hoặc tốt hơn

### Bước 1: Clone Repository

```bash
git clone https://github.com/HungDung2012/baseketball_game.git
cd baseketball_game
```

### Bước 2: Mở Project trong Unity

1. Mở **Unity Hub**
2. Click **"Open"** hoặc **"Add project from disk"**
3. Chọn thư mục `baseketball_game`
4. Đợi Unity import packages (có thể mất vài phút)

### Bước 3: Setup XR

1. Vào **Edit > Project Settings > XR Plug-in Management**
2. Bật **OpenXR** cho platform bạn đang build
3. Vào **OpenXR** settings, thêm **Interaction Profiles** cho headset của bạn

### Bước 4: Build và Chạy

#### **Chạy trong Editor (với XR Device Simulator):**
```
1. Mở scene: Assets/Scenes/New Folder/New Scene.unity
2. Nhấn Play
3. Sử dụng XR Device Simulator để test
```

#### **Build cho Meta Quest:**
```
1. File > Build Settings
2. Switch Platform: Android
3. Player Settings > Other Settings:
   - Minimum API Level: 29 (Android 10)
   - Target API Level: 32+
4. Add scene vào Build Settings
5. Build and Run
```

#### **Build cho PC VR (SteamVR/Oculus):**
```
1. File > Build Settings
2. Platform: Windows
3. Add scene vào Build Settings
4. Build
```

## 📋 Hướng Dẫn Sử Dụng

### Setup Scene
Xem file [`README_SETUP.md`](Assets/Scenes/New%20Folder/README_SETUP.md) để biết hướng dẫn chi tiết về:
- Setup Basketball GameObject
- Setup Basketball Hoop với trigger
- Setup Score UI (World Space Canvas)
- Cấu hình VR Rig và XR Interaction Manager
- Physics settings và materials

### Hiểu Luồng Hoạt Động
Xem file [`LUONG_HOAT_DONG.md`](Assets/Scenes/New%20Folder/LUONG_HOAT_DONG.md) để hiểu:
- Kiến trúc hệ thống
- Flow từ grab → throw → score
- Design patterns sử dụng
- Dependencies và data flow

## 🎯 Scripts Chính

| Script | Chức Năng |
|--------|-----------|
| `BasketballController.cs` | Điều khiển physics và VR grab/throw cho bóng |
| `BasketballHoop.cs` | Phát hiện bóng vào rổ và ghi điểm |
| `HoopTriggerHelper.cs` | Helper script gắn vào trigger zone |
| `ScoreManager.cs` | Quản lý điểm số (Singleton pattern) |
| `BallRespawner.cs` | Spawn và auto-respawn bóng khi rơi |

## 🎨 Customization

### Điều chỉnh Physics
Trong `BasketballController.cs`:
```csharp
public float throwForceMultiplier = 1.5f;  // Tăng = ném mạnh hơn
rb.mass = 0.6f;                            // Khối lượng bóng
rb.drag = 0.1f;                            // Lực cản không khí
```

### Thay đổi Scoring
Trong `BasketballHoop.cs`:
```csharp
public int pointsPerScore = 1;             // Điểm mỗi lần ghi bàn
public float resetDelay = 0.01f;           // Thời gian trước khi reset
```

### Vị trí Spawn
Trong `BallRespawner.cs`:
```csharp
public float respawnHeight = -5f;          // Độ cao auto-respawn
public int maxBalls = 1;                   // Số lượng bóng tối đa
```

## 🐛 Troubleshooting

### Bóng không ghi điểm?
- ✅ Kiểm tra Basketball có tag "Basketball"
- ✅ ScoreTrigger có Collider với "Is Trigger" = true
- ✅ BasketballHoop có gán scoreTriggerZone
- ✅ ScoreManager GameObject tồn tại trong scene

### VR controllers không hoạt động?
- ✅ Kiểm tra XR Interaction Manager trong scene
- ✅ Kiểm tra OpenXR settings đã bật
- ✅ Thêm Interaction Profiles cho headset

### Input System errors?
- ✅ Chạy: `Tools > Fix EventSystem for Input System`
- ✅ Hoặc đổi EventSystem sang Input System UI Input Module

## 📈 Roadmap

- [ ] Thêm combo system (streak scoring)
- [ ] Sound effects (bounce, swoosh, score)
- [ ] Particle effects khi ghi điểm
- [ ] Multiple hoops với độ khó khác nhau
- [ ] Timer mode (60 giây challenge)
- [ ] Training mode với moving targets
- [ ] Multiplayer support
- [ ] Online leaderboard

## 🤝 Contributing

Contributions, issues và feature requests đều được chào đón!

1. Fork repository này
2. Tạo branch mới (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

## 📝 License

Dự án này được phát hành dưới MIT License. Xem file `LICENSE` để biết thêm chi tiết.

## 👨‍💻 Tác Giả

**HungDung2012**
- GitHub: [@HungDung2012](https://github.com/HungDung2012)
- Repository: [baseketball_game](https://github.com/HungDung2012/baseketball_game)

## 🙏 Acknowledgments

- Unity Technologies - XR Interaction Toolkit
- TextMeshPro team
- OpenXR contributors
- VR development community

---

**Made with ❤️ for VR Basketball enthusiasts**

*Nếu thấy project hữu ích, hãy để lại một ⭐ star nhé!*
