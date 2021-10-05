from typing import Optional
from fastapi import FastAPI


api = FastAPI()


@api.get("/")
def get_status():
    return {"status": "ok"}


@api.get("/hello")
def say_hello(name: Optional[str] = None):
    return {"message": f"Hello {name or 'World'}"}


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(api)
