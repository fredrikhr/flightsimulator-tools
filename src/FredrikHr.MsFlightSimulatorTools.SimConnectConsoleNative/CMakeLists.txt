cmake_minimum_required(VERSION 3.26)
cmake_path(GET CMAKE_CURRENT_SOURCE_DIR FILENAME PROJECT_SOURCE_DIR_NAME)
project(
  ${PROJECT_SOURCE_DIR_NAME}
  VERSION ${FredrikHr.MsFlightSimulatorTools_VERSION}
  DESCRIPTION "Native Console application using SimConnect"
  LANGUAGES CXX
)
string(REGEX MATCH "[^\.]+$" PROJECT_NAME_SUFFIX ${PROJECT_NAME})

find_package(SimConnect)

file(GLOB PROJECT_CXX_FILES LIST_DIRECTORIES false *.cpp *.c)
add_executable(${PROJECT_NAME_SUFFIX} ${PROJECT_CXX_FILES})
target_link_libraries(${PROJECT_NAME_SUFFIX} SimConnect::SimConnect)

add_executable("${PROJECT_NAME_SUFFIX}_standalone" ${PROJECT_CXX_FILES})
target_link_libraries("${PROJECT_NAME_SUFFIX}_standalone" SimConnect::SimConnectStaticLibrary)

set_target_properties(${PROJECT_NAME_SUFFIX} "${PROJECT_NAME_SUFFIX}_standalone"
  PROPERTIES
  C_STANDARD 17
  CXX_STANDARD 17
)
