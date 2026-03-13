# Flutter Προπαίδεια

Απλή εφαρμογή Flutter για παιδιά που μαθαίνουν προπαίδεια.

## Γρήγορη εκτέλεση (στον υπολογιστή)

```bash
cd flutter_propedeia
flutter pub get
flutter run
```

## Πώς να το βάλεις σε Android

### 1) Εγκατάσταση εργαλείων
- Flutter SDK
- Android Studio (με Android SDK)
- Ένα Android κινητό ή emulator

Έλεγχος εγκατάστασης:

```bash
flutter doctor
```

### 2) Ενεργοποίηση κινητού (USB Debugging)
1. Στο κινητό: **Settings > About phone > Build number** (πάτησε 7 φορές).
2. Άνοιξε **Developer options**.
3. Ενεργοποίησε **USB debugging**.
4. Σύνδεσε το κινητό με USB και αποδέξου το prompt "Allow USB debugging".

Έλεγχος ότι το βλέπει το Flutter:

```bash
flutter devices
```

### 3) Τρέξιμο εφαρμογής στο κινητό
Από το folder του project:

```bash
cd flutter_propedeia
flutter pub get
flutter run
```

Αν έχεις πολλές συσκευές:

```bash
flutter run -d <device_id>
```

### 4) Δημιουργία APK για εγκατάσταση

```bash
cd flutter_propedeia
flutter build apk --release
```

Το APK θα βγει εδώ:

```text
build/app/outputs/flutter-apk/app-release.apk
```

Μετά το περνάς στο κινητό (USB, Drive, κλπ) και το εγκαθιστάς.

### 5) (Προαιρετικά) App Bundle για Play Store

```bash
flutter build appbundle --release
```

Παράγεται αρχείο `.aab` για ανέβασμα στο Google Play.

---

## Σημείωση
Αν ξεκινάς από μηδενική δομή και λείπουν Android project files, μπορείς να δημιουργήσεις νέο Flutter project και να αντιγράψεις μόνο το `lib/main.dart` σε αυτό.
