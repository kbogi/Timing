namespace UHFDemo
{
    enum NET_CMD
    {
        //通信命令码
        NET_MODULE_CMD_SET = 0X01,     //配置网络中的模块
        NET_MODULE_CMD_GET = 0X02,     //获取某个模块的配置
        NET_MODULE_CMD_RESET = 0x03,     //复位模块
        NET_MODULE_CMD_SEARCH = 0X04,     //搜索网络中的模块
        NET_MODULE_CMD_SET_BASE = 0X05,     //配置模块的端口基本设置
        NET_MODULE_CMD_SET_PORT1 = 0X06,     //配置模块的端口1
        NET_MODULE_CMD_SET_PORT2 = 0X07,     //配置模块的端口2

        NET_MODULE_RESERVE = 0xFF,
    }

    enum NET_ACK
    {
        //应答命令码                 
        NET_MODULE_ACK_SET = 0X81,     //回应配置命令码
        NET_MODULE_ACK_GET = 0X82,     //回应获取命令码
        NET_MODULE_ACK_RESEST = 0X83,     //回应复位命令码
        NET_MODULE_ACK_SEARCH = 0X84,     //回应所搜命令码
        NET_MODULE_ACK_SET_BASE = 0X85,     //配置模块的端口基本设置
        NET_MODULE_ACK_SET_PORT1 = 0X86,     //配置模块的端口1
        NET_MODULE_ACK_SET_PORT2 = 0X87,		//配置模块的端口2
    }

    enum MODULE_TYPE
    {
        //模块标识
        NET_MODULE_TYPE_TCP_S = 0X00,       //模块作为TCP SERVER
        NET_MODULE_TYPE_TCP_C = 0X01,       //模块作为TCP CLIENT
        NET_MODULE_TYPE_UDP_S = 0X02,       //模块作为UDP SERVER
        NET_MODULE_TYPE_UDP_C = 0X03,		//模块作为UDP CLIENT
    }
    
    enum MODULE_VERIFY
    {
        //校验位标识
        NET_MODULE_VERIFY_ODD = 0X00,       //奇校验
        NET_MODULE_VERIFY_EVEN = 0X01,      //偶校验
        NET_MODULE_VERIFY_MARK = 0X02,      //mark校验
        NET_MODULE_VERIFY_SPACE = 0X03,     //space校验
        NET_MODULE_VERIFY_NULL = 0X04,		//无校验
    }

    enum MODULE_CLEAR
    {
        //是否清空串口缓冲区
        NET_MODULE_CLEAR_NO = 0X00,     //不清空缓冲区
        NET_MODULE_CLEAR_TCP = 0X01,		//TCP连接时清空
    }
}