cmake_minimum_required(VERSION 3.27)
get_filename_component(PROJECT_SOURCE_DIR_NAME ${CMAKE_CURRENT_SOURCE_DIR} NAME)
project(
  ${PROJECT_SOURCE_DIR_NAME}
  VERSION ${FredrikHr.MsFlightSimulatorTools_VERSION}
  DESCRIPTION "Native Console application using SimConnect"
  LANGUAGES C
)
string(REGEX MATCH "[^\.]+$" PROJECT_NAME_SUFFIX ${PROJECT_NAME})
file(GLOB PROJECT_C_FILES *.c LIST_DIRECTORIES false)
add_executable(${PROJECT_NAME_SUFFIX} ${PROJECT_C_FILES})
