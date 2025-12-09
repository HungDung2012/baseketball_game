# VR Basketball Game - Hướng Dẫn Setup

Game bóng rổ VR cho Unity 2022.3.60f1 với XR Interaction Toolkit.

## Các Script Đã Tạo

1. **BasketballController.cs** - Điều khiển bóng rổ với VR
2. **ScoreManager.cs** - Quản lý điểm số và combo
3. **BasketballHoop.cs** - Phát hiện khi bóng vào rổ
4. **ScoreUI.cs** - Hiển thị UI điểm số
5. **BallRespawner.cs** - Respawn bóng khi rơi xuống

## Hướng Dẫn Setup Chi Tiết

### 1. Setup Bóng Rổ (Basketball)

1. Tạo GameObject mới: `GameObject > 3D Object > Sphere`
2. Đặt tên: "Basketball"
3. Scale: (0.24, 0.24, 0.24) - kích thước bóng rổ thực tế
4. Thêm tag "Basketball": `Edit > Project Settings > Tags and Layers > Tags > Add "Basketball"`
5. Gán tag cho bóng
6. Add Components:
   - **Rigidbody** (đã được config trong script)
   - **XR Grab Interactable** (đã được config trong script)
   - **BasketballController** script
   - **Audio Source** (đã được config trong script)
   - **Sphere Collider** (nếu chưa có)

7. Tạo Material cho bóng:
   - Tạo Material mới trong Assets
   - Đặt màu cam/nâu cho bóng rổ
   - Gán vào Mesh Renderer của bóng

### 2. Setup Rổ Bóng Rổ (Basketball Hoop)

#### A. Tạo Hoop Structure:
```
BasketballHoop (Empty GameObject)
├── Rim (Torus - vành rổ)
├── Backboard (Cube - bảng)
├── Backboard (Cylinder - lưới)
└── ScoreTrigger (Cylinder - trigger zone)
```

#### B. Chi tiết từng phần:

**Rim (Vành rổ):**
- `GameObject > 3D Object > Torus` (hoặc Cylinder với wall)
- Position: (0, 3.05, 0)
- Scale: (0.45, 0.45, 0.05)
- Thêm **Mesh Collider** hoặc **Box Collider**

**Backboard (Bảng):**
- `GameObject > 3D Object > Cube`
- Position: (0, 3.5, -0.1)
- Scale: (1.8, 1.05, 0.05)
- Thêm **Box Collider**

**ScoreTrigger (Trigger Zone):**
- `GameObject > 3D Object > Cylinder`
- Position: (0, 2.8, 0) - ngay dưới vành rổ
- Scale: (0.4, 0.1, 0.4)
- **BẮT BUỘC**: 
  - Bỏ Mesh Renderer (invisible)
  - Thêm **Collider**
  - **Tick vào "Is Trigger"**

**Gán Script:**
- Thêm **BasketballHoop.cs** vào GameObject "BasketballHoop" (parent)
- Trong Inspector:
  - Score Trigger Zone: Kéo "ScoreTrigger" GameObject vào
  - Points Per Score: 2
  - Ball Reset Position: (0, 1.5, 0)

### 3. Setup Score Manager & UI

#### A. Tạo ScoreManager:
1. Tạo Empty GameObject: "ScoreManager"
2. Add **ScoreManager.cs** script
3. Add **Audio Source** component

#### B. Tạo Canvas UI (World Space cho VR):

1. Tạo Canvas:
   - `GameObject > UI > Canvas`
   - Đặt tên: "ScoreCanvas"
   - Canvas Component:
     - Render Mode: **World Space**
     - Position: (0, 4, 3) - phía trên rổ
     - Rotation: (0, 180, 0)
     - Scale: (0.01, 0.01, 0.01)
     - Width: 400, Height: 300

2. Tạo Text Elements (sử dụng TextMeshPro):
   
   **Score Text:**
   - `Right-click Canvas > UI > Text - TextMeshPro`
   - Đặt tên: "ScoreText"
   - Position: (0, 50, 0)
   - Text: "Score: 0"
   - Font Size: 36
   - Alignment: Center
   - Color: Trắng

   **High Score Text:**
   - Tương tự ScoreText
   - Đặt tên: "HighScoreText"
   - Position: (0, 0, 0)
   - Text: "High Score: 0"
   - Font Size: 28

   **Combo Text:**
   - Tương tự
   - Đặt tên: "ComboText"
   - Position: (0, -50, 0)
   - Text: "Combo x2"
   - Font Size: 32
   - Color: Vàng/Cam

3. Gán UI vào ScoreManager:
   - Chọn GameObject "ScoreManager"
   - Kéo các Text vào các slot tương ứng trong Inspector

#### C. Thêm ScoreUI (Optional):
1. Thêm **ScoreUI.cs** vào "ScoreCanvas" GameObject
2. Gán các Text references
3. Cấu hình Timer nếu muốn (Use Timer = true, Game Duration = 60)

### 4. Setup Ball Respawner

1. Tạo Empty GameObject: "BallSpawner"
2. Position: (0, 1.5, -2) - vị trí spawn bóng
3. Add **BallRespawner.cs** script
4. Trong Inspector:
   - Ball Prefab: Kéo Basketball GameObject vào (tạo prefab trước)
   - Respawn Point: Tự gán (this transform)
   - Respawn Height: -5
   - Allow Multiple Balls: false (hoặc true nếu muốn nhiều bóng)

**Tạo Prefab cho Basketball:**
- Kéo Basketball GameObject vào thư mục Assets/Prefabs
- Xóa Basketball khỏi scene (Spawner sẽ tạo)

### 5. Setup XR Rig (VR Player)

1. Nếu chưa có XR Origin:
   - `GameObject > XR > XR Origin (Action-based)`

2. Đảm bảo có:
   - XR Origin
   - Main Camera (con của XR Origin)
   - LeftHand Controller
   - RightHand Controller

3. Kiểm tra XR Interaction Manager:
   - Tạo Empty GameObject: "XR Interaction Manager"
   - Add component: **XR Interaction Manager**

### 6. Physics Settings

1. `Edit > Project Settings > Physics`
2. Tạo Physics Material cho bóng:
   - `Right-click Assets > Create > Physic Material`
   - Đặt tên: "Basketball_Physics"
   - Dynamic Friction: 0.4
   - Static Friction: 0.4
   - Bounciness: 0.6
   - Gán vào Sphere Collider của bóng

### 7. Audio Setup

1. Tìm hoặc tạo Audio Clips:
   - Bounce Sound (âm thanh bóng nảy)
   - Swoosh Sound (âm thanh bóng vào rổ)
   - Score Sound (âm thanh ghi điểm)

2. Gán Audio:
   - Basketball Controller: Bounce Sound, Swoosh Sound
   - ScoreManager: Score Sound

### 8. Layers Setup (Optional nhưng khuyến nghị)

1. `Edit > Project Settings > Tags and Layers`
2. Tạo layers:
   - Basketball
   - Hoop
   - UI

3. Gán layers cho GameObjects tương ứng




## Troubleshooting

1. **Lỗi Input System (InvalidOperationException):**
   - Lỗi: `You are trying to read Input using the UnityEngine.Input class, but you have switched active Input handling to Input System package`
   - Fix tự động:
     1. Vào menu: `Tools > Fix EventSystem for Input System`
     2. Click OK khi thấy thông báo
     3. Save scene (Ctrl+S)
   - Fix thủ công:
     1. Tìm GameObject "EventSystem" trong Hierarchy
     2. Xóa component "Standalone Input Module"
     3. Add component: `Input System UI Input Module`
     4. Save scene

2. **Bóng không grab được:**
   - Kiểm tra XR Grab Interactable component
   - Kiểm tra XR Interaction Manager có trong scene
   - Kiểm tra Layer Mask của XR Controllers

3. **Không ghi điểm:**
   - Kiểm tra tag "Basketball" đã gán chưa
   - Kiểm tra ScoreTrigger có "Is Trigger" = true
   - Kiểm tra BasketballHoop script có reference đến ScoreTrigger

4. **UI không hiển thị:**
   - Kiểm tra Canvas Render Mode = World Space
   - Kiểm tra vị trí Canvas trong tầm nhìn
   - Kiểm tra Event System có trong scene

5. **Bóng bay quá xa:**
   - Giảm Throw Force Multiplier
   - Tăng Drag của Rigidbody

## Hướng Dẫn Chơi Game (VR Controls)

#### **Kỹ Thuật Ném Hiệu Quả:**

1. **Ném Cơ Bản:**
   - Đứng trước rổ (khoảng 2-3m)
   - Nhặt bóng bằng một tay
   - Ngắm rổ
   - Vung tay từ dưới lên, thả bóng ở đỉnh cao nhất
   - Aim vào rim hoặc backboard

2. **Ném Xa (3-Point):**
   - Đứng xa hơn (4-6m)
   - Dùng cả hai tay để ngắm (optional)
   - Vung mạnh hơn và thả cao hơn
   - Tạo quỹ đạo cong (arc) cao

3. **Layup (Ném sát rổ):**
   - Di chuyển sát rổ
   - Nhặt bóng
   - Nhẹ nhàng đưa bóng lên cao và thả vào rổ
   - Không cần vung mạnh

## Next Steps

1. Thêm visual effects (particles khi ghi điểm)
2. Thêm nhiều rổ với khoảng cách khác nhau
3. Thêm power-ups
4. Thêm multiplayer
5. Thêm training mode với targets
6. Thêm sound effects và background music
