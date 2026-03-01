# OCR για ελληνικό εκκλησιαστικό/παλαιό κείμενο (Tesseract)

## Γιατί οι αριθμοί (19, 20, 21...) «πέφτουν» σε άλλη γραμμή
Στη σελίδα που έστειλες οι αριθμοί είναι στο δεξί περιθώριο (σαν verse markers) και δεν ανήκουν τυπογραφικά στην ίδια baseline με το κυρίως κείμενο.
Ο layout analyzer του Tesseract συχνά τους θεωρεί ξεχωριστό block/column και μετά στην εξαγωγή plain text τους μετακινεί.

## Πώς να πάρεις καλύτερο αποτέλεσμα με Tesseract
1. **Χρησιμοποίησε σωστό μοντέλο γλώσσας**: `ell` (και προαιρετικά `grc` για πολυτονικό/αρχαϊκό ύφος).
2. **Δοκίμασε διαφορετικά PSM**:
   - `--psm 6` για ενιαίο block κειμένου
   - `--psm 4` όταν υπάρχει ξεκάθαρη μονοστηλη σελίδα
   - `--psm 11` για sparse text (λιγότερο καλό για σωστή σειρά)
3. **OCR ανά ζώνες (regions)**: κόψε σελίδα σε:
   - κύριο σώμα κειμένου
   - δεξί περιθώριο με αριθμούς
   και κάνε OCR χωριστά.
4. **Προεπεξεργασία εικόνας**:
   - grayscale
   - ελαφρύ denoise
   - adaptive threshold
   - deskew
   - αύξηση ανάλυσης σε ~300–400 DPI ισοδύναμο
5. **Πάρε TSV/HOCR αντί για σκέτο TXT** και κάνε δικό σου sorting με βάση `top/left`.

## Ενδεικτικές εντολές
```bash
tesseract page.tif out -l ell --oem 1 --psm 6

tesseract page.tif out_tsv -l ell --oem 1 --psm 6 tsv
```

Αν έχεις εγκατεστημένα και τα δύο traineddata:
```bash
tesseract page.tif out -l ell+grc --oem 1 --psm 6
```

## Python: θα έχεις καλύτερο αποτέλεσμα;
**Ναι, συνήθως ναι**, όχι επειδή το OCR engine αλλάζει, αλλά επειδή με Python μπορείς να:
- κάνεις καλύτερο preprocessing (OpenCV/Pillow)
- κάνεις OCR σε υποπεριοχές
- ξαναχτίσεις τη σωστή σειρά κειμένου από TSV boxes
- εφαρμόσεις post-correction λεξικού/κανόνων

## Minimal Python pipeline (OpenCV + pytesseract)
```python
import cv2
import pandas as pd
import pytesseract

img = cv2.imread("page.tif", cv2.IMREAD_GRAYSCALE)
img = cv2.medianBlur(img, 3)
img = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C,
                            cv2.THRESH_BINARY, 35, 11)

# OCR σε TSV για να κρατήσεις συντεταγμένες
cfg = "--oem 1 --psm 6"
tsv = pytesseract.image_to_data(img, lang="ell", config=cfg, output_type=pytesseract.Output.DATAFRAME)

# κράτα μόνο έγκυρες λέξεις
tsv = tsv[tsv.conf.notna()]
tsv = tsv[tsv.conf.astype(float) > 30]

# προαιρετικά: πέτα δεξί περιθώριο αριθμών
W = img.shape[1]
main = tsv[tsv.left < int(W * 0.92)]
margin_nums = tsv[(tsv.left >= int(W * 0.92)) & (tsv.text.str.match(r"^\d+$", na=False))]

# sort πρώτα ανά γραμμή, μετά ανά x
main = main.sort_values(["top", "left"])
text = " ".join(main.text.astype(str).tolist())

print(text)
print("Margin numbers:", margin_nums.text.dropna().tolist())
```

## Υπάρχει καλύτερο OCR από Tesseract;
Για δύσκολο πολυτονικό ελληνικό έντυπο:
- **EasyOCR / PaddleOCR**: συχνά καλύτερα στο detection/layout.
- **Kraken / Calamari**: πολύ καλά αν εκπαιδεύσεις μοντέλο στο δικό σου corpus.
- **Cloud OCR** (Google/Azure/AWS): συνήθως καλύτερο layout+accuracy out-of-the-box, αλλά με κόστος.

Πρακτικά: για βιβλικά/πολυτονικά scans, η μεγαλύτερη βελτίωση έρχεται από **σωστό segmentation + preprocessing + post-correction**, όχι μόνο από αλλαγή engine.
