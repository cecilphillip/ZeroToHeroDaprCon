# FastAPI and the Dapr CLI - DaprCon Demo
This sample shows how to daper-ize a Python Web API. It makes use of the `dapr` CLI command to interact with the locally running application.

## Requirements
You'll need to have the follow installed to propperly run the demo.
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [Python](https://www.python.org/downloads/)

## Spinning up the environment
Run these commands in the demo2 root directory.
First, you'll create a virtual environment for the workspace.

```bash
> python -m venv .venv 
```
Once this is complete, activate virtual environment. If you're using VS Code with the python extension installed, it usually prompts you to activate the environment for you.

```bash
> . .venv/bin/activate
```

Run the following commands to upgrade pip and install FastAI
```bash
> python -m pip install --upgrade pip
> python -m pip install 'fastapi[all]' # quotes for zsh users
```

### Running the application without Dapr
```bash
> python main.py
```

### Running the application using the Dapr CLI
```bash
dapr run --app-id api --app-port 8000 --dapr-http-port 3500 python main.py
```

### Making API requests
Sample requests have been provided in the [httprequests](httprequests/) folder that work with the [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) for VS Code, but you can use any client you like such as cURL.
