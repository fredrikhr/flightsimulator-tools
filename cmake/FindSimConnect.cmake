if(NOT MSFS_SDK_FOUND)
  if(DEFINED PACKAGE_FIND_VERSION_RANGE)
    find_package(MSFS_SDK ${PACKAGE_FIND_VERSION_RANGE})
  elseif(DEFINED PACKAGE_FIND_VERSION)
    find_package(MSFS_SDK ${PACKAGE_FIND_VERSION})
  else()
    find_package(MSFS_SDK)
  endif()
endif(NOT MSFS_SDK_FOUND)
if(MSFS_SDK_FOUND)
  set(SimConnect_ROOT_DIR "${MSFS_SDK_ROOT_DIR}/SimConnect SDK")
  if(IS_DIRECTORY ${SimConnect_ROOT_DIR})
    set(SimConnect_FOUND TRUE)
    set(SimConnect_VERSION ${MSFS_SDK_VERSION})
    find_path(SimConnect_INCLUDE_DIR
      NAMES SimConnect.h
      PATHS ${SimConnect_ROOT_DIR}
      PATH_SUFFIXES include
    )
    find_library(SimConnect_LIBRARY
      NAMES SimConnect
      PATHS ${SimConnect_ROOT_DIR}
      PATH_SUFFIXES lib
    )
  endif(IS_DIRECTORY ${SimConnect_ROOT_DIR})
endif(MSFS_SDK_FOUND)

include(FindPackageHandleStandardArgs)
find_package_handle_standard_args(SimConnect
  FOUND_VAR SimConnect_FOUND
  REQUIRED_VARS
    SimConnect_ROOT_DIR
    SimConnect_INCLUDE_DIR
    SimConnect_LIBRARY
  VERSION_VAR SimConnect_VERSION
  HANDLE_VERSION_RANGE
)
