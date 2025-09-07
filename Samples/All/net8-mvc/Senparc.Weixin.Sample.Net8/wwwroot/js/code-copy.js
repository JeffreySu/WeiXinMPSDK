function copyCode(button) {
    // Find the code element within the same container
    const container = button.closest('.code-container');
    const codeElement = container.querySelector('code');
    
    // Get the text content
    const text = codeElement.textContent;
    
    // Create a temporary textarea element to copy the text
    const textarea = document.createElement('textarea');
    textarea.value = text;
    textarea.style.position = 'fixed';  // Prevent scrolling to bottom
    document.body.appendChild(textarea);
    textarea.select();
    
    try {
        // Execute the copy command
        document.execCommand('copy');
        
        // Visual feedback
        button.classList.add('success');
        
        // Reset the button state after animation
        setTimeout(() => {
            button.classList.remove('success');
        }, 2000);
    } catch (err) {
        console.error('Failed to copy text: ', err);
    } finally {
        // Cleanup
        document.body.removeChild(textarea);
    }
}
