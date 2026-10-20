import base64

from cryptography.hazmat.primitives import padding
from cryptography.hazmat.primitives.ciphers import Cipher, algorithms, modes


def decrypt_aes_128_cbc(ciphertext: bytes, key: bytes, iv: bytes) -> bytes:
    """Match Node: createDecipheriv("aes-128-cbc", key.slice(0, 16), iv) with auto padding."""
    key16 = key[:16]
    if len(key16) != 16:
        raise ValueError("key must be at least 16 bytes")
    if len(iv) != 16:
        raise ValueError("IV must be 16 bytes")
    if len(ciphertext) == 0 or len(ciphertext) % 16 != 0:
        raise ValueError("ciphertext length must be a positive multiple of 16")

    decryptor = Cipher(algorithms.AES(key16), modes.CBC(iv)).decryptor()
    padded = decryptor.update(ciphertext) + decryptor.finalize()
    unpadder = padding.PKCS7(128).unpadder()
    return unpadder.update(padded) + unpadder.finalize()


def decrypt_text(encrypted: str, key: bytes, iv: bytes, encoding: str = "base64") -> str:
    if encoding == "base64":
        ciphertext = base64.b64decode(encrypted)
    elif encoding == "hex":
        ciphertext = bytes.fromhex(encrypted)
    else:
        raise ValueError("encoding must be 'base64' or 'hex'")
    return decrypt_aes_128_cbc(ciphertext, key, iv)

def decrypt_from_text(encrypted: str) -> str:
    key = b"!WeBmAsTeR@iNoOr.Ir-(1393,2014)!"  # same bytes as getKeyBuffer(); only the first 16 are used
    iv = b"!C.R.C.I.S-key#!"       # same 16 bytes as getIVBuffer()

    return decrypt_text(encrypted, key, iv)

