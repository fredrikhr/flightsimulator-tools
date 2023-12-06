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
  set(SimConnect_ROOT_DIR "${MSFS_SDK_ROOT_DIR}/SimConnect SDK"
    CACHE PATH "SimConnect SDK root directory path"
  )
  if(IS_DIRECTORY "${SimConnect_ROOT_DIR}")
    set(SimConnect_FOUND TRUE)
    set(SimConnect_VERSION ${MSFS_SDK_VERSION})
    find_path(SimConnect_INCLUDE_DIR
      NAMES SimConnect.h
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES include
      DOC "SimConnect include directory path"
    )
    find_file(SimConnect_LIBRARY
      NAMES SimConnect.dll
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES lib
      DOC "SimConnect shared library file path"
    )
    find_file(SimConnect_IMPORT_LIBRARY
      NAMES SimConnect.lib
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES lib
      DOC "SimConnect shared import library file path"
    )
    find_file(SimConnect_STATIC_LIBRARY
      NAMES SimConnect.lib
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES "lib/static"
      DOC "SimConnect static library file path"
    )
    find_file(SimConnect_STATIC_LIBRARY_DEBUG
      NAMES SimConnect_debug.lib
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES "lib/static"
      DOC "SimConnect static library file path (Debug)"
    )
    find_file(SimConnect_MANAGED_LIBRARY
      NAMES Microsoft.FlightSimulator.SimConnect.dll
      PATHS "${SimConnect_ROOT_DIR}"
      PATH_SUFFIXES "lib/managed"
      DOC "SimConnect managed .NET library file path"
    )
  endif(IS_DIRECTORY "${SimConnect_ROOT_DIR}")
endif(MSFS_SDK_FOUND)

include(FindPackageHandleStandardArgs)
find_package_handle_standard_args(SimConnect
  FOUND_VAR SimConnect_FOUND
  REQUIRED_VARS
    SimConnect_ROOT_DIR
    SimConnect_INCLUDE_DIR
    SimConnect_LIBRARY
    SimConnect_IMPORT_LIBRARY
    SimConnect_STATIC_LIBRARY
    SimConnect_STATIC_LIBRARY_DEBUG
    SimConnect_MANAGED_LIBRARY
  VERSION_VAR SimConnect_VERSION
  HANDLE_VERSION_RANGE
)
mark_as_advanced(
  SimConnect_INCLUDE_DIR
  SimConnect_LIBRARY
  SimConnect_IMPORT_LIBRARY
  SimConnect_STATIC_LIBRARY
  SimConnect_STATIC_LIBRARY_DEBUG
  SimConnect_MANAGED_LIBRARY
)

if(SimConnect_FOUND)
  if(NOT TARGET SimConnect::SimConnect)
    add_library(SimConnect::SimConnect SHARED IMPORTED)
    set_target_properties(SimConnect::SimConnect PROPERTIES
      IMPORTED_LOCATION "${SimConnect_LIBRARY}"
      IMPORTED_IMPLIB "${SimConnect_IMPORT_LIBRARY}"
    )
    target_include_directories(SimConnect::SimConnect
      INTERFACE "${SimConnect_INCLUDE_DIR}"
    )
  endif()
  if(NOT TARGET SimConnect::SimConnectStaticLibrary)
    add_library(SimConnect::SimConnectStaticLibrary STATIC IMPORTED)
    set_target_properties(SimConnect::SimConnectStaticLibrary PROPERTIES
      IMPORTED_LOCATION "${SimConnect_STATIC_LIBRARY}"
      IMPORTED_LOCATION_DEBUG "${SimConnect_STATIC_LIBRARY_DEBUG}"
    )
    target_include_directories(SimConnect::SimConnectStaticLibrary
      INTERFACE "${SimConnect_INCLUDE_DIR}"
    )
    target_link_libraries(SimConnect::SimConnectStaticLibrary
      INTERFACE shlwapi.lib user32.lib Ws2_32.lib
    )
  endif()
  if(NOT TARGET SimConnect::SimConnectManagedLibrary)
    add_library(SimConnect::SimConnectManagedLibrary MODULE IMPORTED)
    set_target_properties(SimConnect::SimConnectManagedLibrary PROPERTIES
      IMPORTED_LOCATION "${SimConnect_MANAGED_LIBRARY}"
    )
  endif()
endif(SimConnect_FOUND)
