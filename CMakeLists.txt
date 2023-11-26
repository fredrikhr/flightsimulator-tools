cmake_minimum_required(VERSION 3.27)
project(FredrikHr.MsFlightSimulatorTools VERSION 0.0.0)

list(PREPEND CMAKE_MODULE_PATH ${CMAKE_CURRENT_SOURCE_DIR}/cmake)
find_package(SimConnect)

foreach(REPO_SRC_PROJECT_NAME IN ITEMS
  FredrikHr.MsFlightSimulatorTools.SimConnectConsoleNative
  )
  add_subdirectory(
    src/${REPO_SRC_PROJECT_NAME}
    ${REPO_SRC_PROJECT_NAME}
  )
endforeach(REPO_SRC_PROJECT_NAME)
