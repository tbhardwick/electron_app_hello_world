{
  "targets": [
    {
      "target_name": "bti_addon",
      "sources": [ "src/addon.cpp" ],
      "include_dirs": [
        "vendor/include",
        "<!@(node -p \"require('node-addon-api').include\")"
      ],
      'defines': [ 'NAPI_DISABLE_CPP_EXCEPTIONS' ],
      "conditions": [
        ['OS=="win"', {
          "libraries": [
            "-l../vendor/lib/win-x64/BTI42964.LIB",
            "-l../vendor/lib/win-x64/BTICARD64.LIB"
          ],
          "copies": [
            {
              "files": [
                "vendor/bin/win-x64/BTI42964.DLL",
                "vendor/bin/win-x64/BTICARD64.DLL"
              ],
              "destination": "<(PRODUCT_DIR)"
            }
          ],
          "msvs_settings": {
            "VCCLCompilerTool": {
              "ExceptionHandling": 1
            }
          }
        }]
      ]
    }
  ]
} 