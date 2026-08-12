

export const Container = ({children, ...props}) => {
    return (
        <div className="min-h-screen bg-[#021526] text-[#E2E2B6]" {...props}>
            {children}
        </div>
    )
}