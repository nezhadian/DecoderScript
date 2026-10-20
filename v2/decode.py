import base64
import os
import struct
import zlib

from decrypt import decrypt_from_text

# Constants
subtraction_value = 0
is_value_set = False
NEWLINE_REPLACEMENT_FOR_WINDOWS_ONLY = "\r\n"

file = r"D:\Projects\SimpleTools\Simple Decoder\encode_text.txt"

# Step 1: decode base64
with open(file, 'r', encoding='utf-8') as f:
    base64_source_file = f.read().replace("_", "/").replace("-", "+")

# Add padding if needed
mod4 = len(base64_source_file) % 4
if mod4 > 0:
    base64_source_file += '=' * (4 - mod4)

# Decode base64
# decoded_bytes = base64.b64decode(base64_source_file)
# text = decoded_bytes.decode('utf-8')

text = decrypt_from_text(base64_source_file)


def decompress_data(data: bytes) -> bytes:
    if len(data) < 4:
        raise ValueError("Input data is too short")
    expected = struct.unpack(">I", data[:4])[0]
    out = zlib.decompress(data[4:])
    if len(out) != expected:
        raise ValueError(
            f"Decompressed size {len(out)} does not match expected {expected}"
        )
    return out

if isinstance(text, str):
    text = text.encode("latin-1")  # only if something turned it into a string

decompressed = decompress_data(text)
result_text = decompressed.decode("utf-8")

# Step 4: combine everything and show the result
with open("output.txt", "w", encoding="utf-8") as f:
    f.write(result_text)

print("we are done!!!")
