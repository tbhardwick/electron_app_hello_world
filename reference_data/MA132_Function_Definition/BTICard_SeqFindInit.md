# **SeqFindInit**

```
ERRVAL BTICard_SeqFindInit
(
     LPUSHORT seqbuf, //Pointer to a Sequential Record buffer
     ULONG seqcount, //Number of 16-bit words in the buffer
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```
# **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Initializes the structure (*sfinfo*) used by other BTICard\_SeqFindNext?? functions for finding records within a Sequential Record buffer. *seqbuf* is a pointer to the start of a buffer containing Sequential Record data and *seqcount* is the number of 16-bit words in the buffer.

*sfinfo* contains information that is used by the various BTICard\_SeqFindNext?? functions. Each time a BTICard\_SeqFindNext?? function is called, the *sfinfo* structure is updated to indicate where to resume the search with the next BTICard\_SeqFindNext?? function. Since these find functions pick up where they left off, based on *sfinfo*, it is necessary to call BTICard\_SeqFindInit whenever a find function is to start at the beginning of the buffer.

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

This function causes the BTICard\_SeqFindNext?? functions to start their search at the beginning of the Sequential Record buffer.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindNext??